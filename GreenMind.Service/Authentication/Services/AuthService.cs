using Google.Apis.Auth;
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

            return _jwtService.GenerateToken(user.Id.ToString(), user.Email, "User");
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
           
               

     
        
  

            if (admin != null)
                return _jwtService.GenerateToken(admin.Email, "Admin");

            throw new Exception("Invalid Email or Password");
        }
    }
}