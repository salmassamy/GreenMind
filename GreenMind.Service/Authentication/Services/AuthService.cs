using Google.Apis.Auth;
using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.Service.Authentication.DTOs;
using GreenMind.ServiceAbstraction.Authentication;
using GreenMind.ServiceAbstraction.Authentication.DTOs;
using GreenMind.ServiceAbstraction.DTOs; 
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GreenMind.Service.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthService(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<string> RegisterUserAsync(RegisterUserDto dto)
        {
            var email = dto.Email.ToLower();
            if (await _context.Users.AnyAsync(x => x.Email.ToLower() == email))
                throw new Exception("Email already exists");

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = _hasher.Hash(dto.Password),
                CreatedDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _jwtService.GenerateToken(user.Id.ToString(), user.Email, "User");
        }

        public async Task<AuthResponseDto> GoogleLoginAsync(string token, string role)
        {
            var email = dto.Email.ToLower();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email && x.Password == dto.Password);

            if (user != null)
                return _jwtService.GenerateToken(user.Id.ToString(), user.Email, "User");

            throw new Exception("Invalid Email or Password");
        }

        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            await Task.CompletedTask;
        }

        public async Task<AuthResponseDto> ExternalLoginAsync(string name, string role)
        {
            return await Task.FromResult(new AuthResponseDto());
        }
    }
}
