using GreenMind.Domain.Entities;
using GreenMind.Presistance.DataSeed;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GreenMind.Presistance.Data.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ChatLog> ChatLogs { get; set; }
        public DbSet<Article> Articles { get; set; }
      
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public DbSet<UserActivityHistory> UserActivityHistory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. إضافة الأدمن أوتوماتيكياً (Seeding)
            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = 1,
                Name = "SalmaAdmin",
                Email = "admin01@gmail.com",
                Password = "AQAAAAEAACcQAAAAEBy9Mjk9Z3lR5jL2PqX9H3L0T4M5Z6X7qG9zF9vL2K8W7M5Z6X7",
                CreatedDate = DateTime.Now
            });

            // 2. حل مشكلة الـ Cascade Path
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Address)
                .WithMany()
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.NoAction);

            // 3. حل رسايل الـ Warnings بتاعة الـ Decimal
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                        .SelectMany(t => t.GetProperties())
                        .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            // 4. السطر اللي هيفعل كلاس الـ Seed بتاع المنتجات
            modelBuilder.ApplyConfiguration(new CategorySeed()); 
            modelBuilder.ApplyConfiguration(new ProductSeed());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
           
            optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }
    }
}
