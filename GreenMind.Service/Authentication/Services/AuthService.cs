using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.Service.Authentication.DTOs;
using GreenMind.ServiceAbstraction.Authentication;
using GreenMind.ServiceAbstraction.Authentication.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using GreenMind.ServiceAbstraction.Interfaces;
namespace GreenMind.Service.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;
        private readonly IPasswordHasherService _hasher;

        public AuthService(
            ApplicationDbContext context,
            JwtService jwtService,
            IPasswordHasherService hasher)
        {
            _context = context;
            _jwtService = jwtService;
            _hasher = hasher;
        }

        // =========================
        // Register User (Only User signup)
        // =========================
        public async Task<AuthResponseDto> RegisterUserAsync(RegisterUserDto dto)
        {
            var email = dto.Email.Trim().ToLower();
            var name = dto.Name.Trim();

            // Email unique across Users & Admins
            var emailExists =
                await _context.Users.AnyAsync(x => x.Email.ToLower() == email) ||
                await _context.Admins.AnyAsync(x => x.Email.ToLower() == email);

            if (emailExists)
                throw new AuthHttpException(400, "Email already exists");

            // UserName unique across Users & Admins
            var nameExists =
                await _context.Users.AnyAsync(x => x.Name == name) ||
                await _context.Admins.AnyAsync(x => x.Name == name);

            if (nameExists)
                throw new AuthHttpException(400, "UserName already exists");

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = _hasher.Hash(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user.Id, user.Email, "User");

            return new AuthResponseDto
            {
                Token = token,
                UserName = user.Name,
                Role = "User"
            };
        }

        // =========================
        // Login (Role + Email OR UserName)
        // =========================
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var key = dto.Email.Trim();
            var role = dto.Role.Trim();

            if (role != "User" && role != "Admin")
                throw new AuthHttpException(400, "Invalid role");

            if (role == "User")
            {
                // role mismatch: exists in Admins => 403
                var adminMismatch = await _context.Admins
                    .AnyAsync(a => a.Email.ToLower() == key.ToLower() || a.Name == key);

                if (adminMismatch)
                    throw new AuthHttpException(403, "Forbidden: role mismatch");

                var user = await _context.Users.FirstOrDefaultAsync(u =>
                    u.Email.ToLower() == key.ToLower() || u.Name == key);

                if (user == null)
                    throw new AuthHttpException(401, "Invalid Email/UserName or Password");

                if (!_hasher.Verify(user.PasswordHash, dto.Password))
                    throw new AuthHttpException(401, "Invalid Email/UserName or Password");

                var token = _jwtService.GenerateToken(user.Id, user.Email, "User");
                return new AuthResponseDto { Token = token, UserName = user.Name, Role = "User" };
            }
            else
            {
                // role == Admin
                // role mismatch: exists in Users => 403
                var userMismatch = await _context.Users
                    .AnyAsync(u => u.Email.ToLower() == key.ToLower() || u.Name == key);

                if (userMismatch)
                    throw new AuthHttpException(403, "Forbidden: role mismatch");

                var admin = await _context.Admins.FirstOrDefaultAsync(a =>
                    a.Email.ToLower() == key.ToLower() || a.Name == key);

                if (admin == null)
                    throw new AuthHttpException(401, "Invalid Email/UserName or Password");

                if (!_hasher.Verify(admin.PasswordHash, dto.Password))
                    throw new AuthHttpException(401, "Invalid Email/UserName or Password");

                var token = _jwtService.GenerateToken(admin.Id, admin.Email, "Admin");
                return new AuthResponseDto { Token = token, UserName = admin.Name, Role = "Admin" };
            }
        }
        public async Task<AuthResponseDto> ExternalLoginAsync(string email, string name, string role)
        {
            email = email.Trim().ToLower();
            name = name.Trim();

            if (role != "User" && role != "Admin")
                throw new AuthHttpException(400, "Invalid role");

            if (role == "Admin")
            {
                var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email.ToLower() == email);
                if (admin == null)
                    throw new AuthHttpException(403, "Forbidden: not an admin account");

                var token = _jwtService.GenerateToken(admin.Id, admin.Email, "Admin");
                return new AuthResponseDto { Token = token, UserName = admin.Name, Role = "Admin" };
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);

            if (user == null)
            {
                user = new User
                {
                    Name = name,
                    Email = email,
                    PasswordHash = _hasher.Hash(Guid.NewGuid().ToString())
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            var userToken = _jwtService.GenerateToken(user.Id, user.Email, "User");
            return new AuthResponseDto { Token = userToken, UserName = user.Name, Role = "User" };
        }
        // =========================
        // Forgot Password (Generate Token)
        // =========================
        public async Task ForgotPasswordAsync(ForgotPasswordRequestDto dto)
        {
            var email = dto.Email.Trim().ToLower();
            var role = dto.Role.Trim();

            if (role != "User" && role != "Admin")
                throw new AuthHttpException(400, "Invalid role");

            var exists = role == "User"
                ? await _context.Users.AnyAsync(x => x.Email.ToLower() == email)
                : await _context.Admins.AnyAsync(x => x.Email.ToLower() == email);

            // Security: return same response
            if (!exists)
                return;

            var token = GenerateSecureToken(48);

            _context.PasswordResetTokens.Add(new PasswordResetToken
            {
                Email = email,
                Role = role,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                IsUsed = false,
                CreatedDate = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

        // =========================
        // Reset Password
        // =========================
        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var role = dto.Role.Trim();

            var prt = await _context.PasswordResetTokens
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(x =>
                    x.Token == dto.Token &&
                    x.Role == role &&
                    !x.IsUsed &&
                    x.ExpiresAt > DateTime.UtcNow);

            if (prt == null)
                throw new AuthHttpException(400, "Invalid or expired token");

            if (role == "User")
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == prt.Email);
                if (user == null) throw new AuthHttpException(400, "User not found");

                user.PasswordHash = _hasher.Hash(dto.NewPassword);
            }
            else
            {
                var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Email.ToLower() == prt.Email);
                if (admin == null) throw new AuthHttpException(400, "Admin not found");

                admin.PasswordHash = _hasher.Hash(dto.NewPassword);
            }

            prt.IsUsed = true;
            await _context.SaveChangesAsync();
        }

        private static string GenerateSecureToken(int bytesLength)
        {
            var bytes = RandomNumberGenerator.GetBytes(bytesLength);
            return Convert.ToBase64String(bytes)
                .Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
    }


    public class AuthHttpException : Exception
    {
        public int StatusCode { get; }
        public AuthHttpException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}