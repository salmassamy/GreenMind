using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenMindAI.DataSeed
{
    public static class AdminSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, IPasswordHasherService hasher)
        {
          
            var exists = await context.Admins.AnyAsync(a => a.Email.ToLower() == "admin@greenmind.com");
            if (exists) return;

            var admin = new Admin
            {
                Name = "SuperAdmin",
                Email = "admin@gmail.com",
                PasswordHash = hasher.Hash("Admin123!"),
                CreatedDate = DateTime.UtcNow
            };

            context.Admins.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}