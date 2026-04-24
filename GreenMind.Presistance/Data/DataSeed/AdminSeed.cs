using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.ServiceAbstraction.Interfaces; // تأكد من المسار الصحيح للـ Interface بتاعك
using Microsoft.EntityFrameworkCore; // ضروري جداً عشان AnyAsync تشتغل
using System;
using System.Threading.Tasks;

namespace GreenMind.Presistance.Data.DataSeed
{
    // 1. تأكد أنها public عشان تقدر تشوفها من الـ Program.cs
    public class AdminSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, IPasswordHasherService hasher)
        {
            var adminEmail = "admin@greenmind.com";

            // 2. AnyAsync محتاجة using Microsoft.EntityFrameworkCore فوق
            var exists = await context.Admins.AnyAsync(a => a.Email == adminEmail);

            if (!exists)
            {
                var admin = new Admin
                {
                    // 3. حل مشاكل الـ Null بوضع قيم افتراضية
                    Name = "Super Admin",
                    Email = adminEmail,
                    Password = hasher.Hash("Admin@123")
                };

                await context.Admins.AddAsync(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}