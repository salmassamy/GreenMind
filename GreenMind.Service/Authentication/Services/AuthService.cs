using Google.Apis.Auth;
using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.Service.Authentication.DTOs;
using GreenMind.ServiceAbstraction.Authentication;
using GreenMind.ServiceAbstraction.Authentication.DTOs;
using GreenMind.ServiceAbstraction.DTOs;
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GreenMind.Service.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;
        private readonly IPasswordHasherService _hasher;
        private readonly IMemoryCache _cache;
        private readonly IEmailService _emailService;
        private readonly ISocialAuthService _socialAuthService;

        public AuthService(
            ApplicationDbContext context,
            JwtService jwtService,
            IPasswordHasherService hasher,
            IMemoryCache cache,
            IEmailService emailService,
            ISocialAuthService socialAuthService)
        {
            _context = context;
            _jwtService = jwtService;
            _hasher = hasher;
            _cache = cache;
            _emailService = emailService;
            _socialAuthService = socialAuthService;
        }

        private string NormalizeEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new AuthHttpException(400, "Email is required");

            return email.Trim().ToLower();
        }

        private string NormalizeRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new AuthHttpException(400, "Role is required");

            role = role.Trim();

            if (!role.Equals("User", StringComparison.OrdinalIgnoreCase) &&
                !role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                throw new AuthHttpException(400, "Role must be User or Admin");
            }

            return role;
        }

        private string GenerateOtp()
        {
            var rnd = new Random();
            return rnd.Next(100000, 999999).ToString();
        }

        private string GetOtpCacheKey(string role, string email)
        {
            return $"otp:{role.ToLower()}:{email.ToLower()}";
        }

        private void SaveOtp(string role, string email, string otp)
        {
            var key = GetOtpCacheKey(role, email);

            _cache.Set(key, otp, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
        }

        private string? GetOtp(string role, string email)
        {
            var key = GetOtpCacheKey(role, email);
            _cache.TryGetValue(key, out string? otp);
            return otp;
        }

        private Task InvalidateOtpAsync(string role, string email)
        {
            var key = GetOtpCacheKey(role, email);
            _cache.Remove(key);
            return Task.CompletedTask;
        }

        public async Task<AuthResponseDto> RegisterUserAsync(RegisterUserDto dto)
        {
            var email = NormalizeEmail(dto.Email);
            var name = dto.Name.Trim();

            var emailExists =
                await _context.Users.AnyAsync(x => x.Email.ToLower() == email) ||
                await _context.Admins.AnyAsync(x => x.Email.ToLower() == email);

            if (emailExists)
                throw new AuthHttpException(400, "Email already exists");

            var nameExists =
                await _context.Users.AnyAsync(x => x.Name == name) ||
                await _context.Admins.AnyAsync(x => x.Name == name);

            if (nameExists)
                throw new AuthHttpException(400, "UserName already exists");

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new AuthHttpException(400, "Password is required");
           if (dto.Password.Length < 6)
                throw new AuthHttpException(400, "Password must be at least 6 characters");

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = _hasher.Hash(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user.Email, "User", user.Id, user.Name);

            return new AuthResponseDto
            {
                Token = token,
                UserName = user.Name,
                Role = "User"
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var key = dto.Email?.Trim().ToLower();
            var role = NormalizeRole(dto.Role);

            if (string.IsNullOrWhiteSpace(key))
                throw new AuthHttpException(400, "Email/UserName is required");

            // ================= USER =================
            if (role.Equals("User", StringComparison.OrdinalIgnoreCase))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u =>
                    u.Email.ToLower() == key || u.Name.ToLower() == key);

                if (user == null)
                    throw new AuthHttpException(401, "Invalid Email/UserName or Password");

                if (!_hasher.Verify(user.PasswordHash, dto.Password))
                    throw new AuthHttpException(401, "Invalid Email/UserName or Password");

                _context.UserActivityLogs.Add(new UserActivityLog
                {
                    UserName = user.Name,
                    ActionType = "User Login",
                    StartedAt = DateTime.Now
                });

                await _context.SaveChangesAsync();

                return new AuthResponseDto
                {
                    Token = _jwtService.GenerateToken(user.Email, "User", user.Id, user.Name),
                    UserName = user.Name,
                    Role = "User"
                };
            }

            // ================= ADMIN =================
            var admin = await _context.Admins.FirstOrDefaultAsync(a =>
                a.Email.ToLower() == key || a.Name.ToLower() == key);

            if (admin == null)
                throw new AuthHttpException(401, "Invalid Email/UserName or Password");

            if (!_hasher.Verify(admin.Password, dto.Password))
                throw new AuthHttpException(401, "Invalid Email/UserName or Password");

            _context.UserActivityLogs.Add(new UserActivityLog
            {
                UserName = admin.Name,
                ActionType = "Admin Login",
                StartedAt = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = _jwtService.GenerateToken(admin.Email, "Admin", admin.Id, admin.Name),
                UserName = admin.Name,
                Role = "Admin"
            };
        }
        public async Task<string> ForgotPasswordAsync(ForgotPasswordRequestDto dto)
        {
            var email = NormalizeEmail(dto.Email);
            var role = NormalizeRole(dto.Role);

            if (!role.Equals("User", StringComparison.OrdinalIgnoreCase))
                throw new AuthHttpException(403, "Forgot password allowed for users only");

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email);

            if (user == null)
                throw new AuthHttpException(404, "User not found");

            var otp = GenerateOtp();

            SaveOtp(role, email, otp);

            await _emailService.SendEmailAsync(
                email,
                "Reset Password OTP",
                $"Your OTP code is: {otp}"
            );

            return "OTP sent successfully.";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var email = NormalizeEmail(dto.Email);
            var role = NormalizeRole(dto.Role);
             if (!role.Equals("User", StringComparison.OrdinalIgnoreCase))
                throw new AuthHttpException(403, "Reset password allowed for users only");

            if (string.IsNullOrWhiteSpace(dto.Otp))
                throw new AuthHttpException(400, "OTP is required");

            if (string.IsNullOrWhiteSpace(dto.NewPassword))
                throw new AuthHttpException(400, "NewPassword is required");

            if (string.IsNullOrWhiteSpace(dto.ConfirmNewPassword))
                throw new AuthHttpException(400, "ConfirmPassword is required");

            if (dto.NewPassword != dto.ConfirmNewPassword)
                throw new AuthHttpException(400, "Passwords do not match");

            var savedOtp = GetOtp(role, email);

            if (savedOtp == null)
                throw new AuthHttpException(400, "OTP expired or not found");

            if (!string.Equals(savedOtp, dto.Otp.Trim(), StringComparison.Ordinal))
                throw new AuthHttpException(400, "Invalid OTP");

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email);

            if (user == null)
                throw new AuthHttpException(404, "User not found");

            user.PasswordHash = _hasher.Hash(dto.NewPassword);

            await _context.SaveChangesAsync();
            await InvalidateOtpAsync(role, email);

            return "Password reset successfully.";
        }

        public async Task<AuthResponseDto> GoogleLoginAsync(string token, string role)
        {
            role = NormalizeRole(role);

            if (!role.Equals("User", StringComparison.OrdinalIgnoreCase))
                throw new AuthHttpException(400, "Google login is allowed for User only.");

            var payload = await GoogleJsonWebSignature.ValidateAsync(token);
            var email = payload.Email.Trim().ToLower();
            var name = string.IsNullOrWhiteSpace(payload.Name) ? "User" : payload.Name;

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email);

            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    Name = name,
                    PasswordHash = _hasher.Hash(Guid.NewGuid().ToString())
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            var jwtToken = _jwtService.GenerateToken(user.Email, "User", user.Id, user.Name);

            return new AuthResponseDto
            {
                Token = jwtToken,
                UserName = user.Name,
                Role = "User"
            };
        }

        public async Task<AuthResponseDto> FacebookLoginAsync(string token, string role)
        {
            role = NormalizeRole(role);

            if (!role.Equals("User", StringComparison.OrdinalIgnoreCase))
                throw new AuthHttpException(400, "Facebook login is allowed for User only.");

            var result = await _socialAuthService.VerifyFacebookAsync(token);
            var email = result.Email?.Trim().ToLower();
            var name = string.IsNullOrWhiteSpace(result.Name) ? "User" : result.Name;

            if (string.IsNullOrWhiteSpace(email))
            {
                email = $"{Guid.NewGuid():N}@facebook.local";
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email);

            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    Name = name,
                    PasswordHash = _hasher.Hash(Guid.NewGuid().ToString())
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            var jwtToken = _jwtService.GenerateToken(user.Email, "User", user.Id, user.Name);
            return new AuthResponseDto
            {
                Token = jwtToken,
                UserName = user.Name,
                Role = "User"
            };
        }
    }
}