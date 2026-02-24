using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.Service.Authentication.DTOs;
using GreenMind.ServiceAbstraction.Authentication;
using Microsoft.EntityFrameworkCore;

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
            var email = dto.Email.Trim().ToLower();

            if (await _context.Users.AnyAsync(x => x.Email.ToLower() == email))
                throw new Exception("Email already exists");

            var user = new User
            {
                Name = dto.Name,
                Email = email,
                Password = dto.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _jwtService.GenerateToken(user.Email, "User");
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var email = dto.Email.Trim().ToLower();

            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Email.ToLower() == email &&
                    x.Password == dto.Password);

            if (user != null)
                return _jwtService.GenerateToken(user.Email, "User");

            var admin = await _context.Admins
                .FirstOrDefaultAsync(x =>
                    x.Email.ToLower() == email &&
                    x.Password == dto.Password);

            if (admin != null)
                return _jwtService.GenerateToken(admin.Email, "Admin");

            throw new Exception("Invalid Email or Password");
        }
    }
}