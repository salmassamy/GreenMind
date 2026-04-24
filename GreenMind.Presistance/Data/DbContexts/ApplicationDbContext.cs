using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.Seed;
using GreenMind.Presistance.DataSeed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

            // ================= 1. التعديلات العامة لكل الجداول (التعديل الجديد) =================
            var allEntities = modelBuilder.Model.GetEntityTypes();

            foreach (var entityType in allEntities)
            {
                // أ. جعل كل الحقول النصية (string) تسمح بـ NULL بشكل افتراضي
                foreach (var property in entityType.GetProperties().Where(p => p.ClrType == typeof(string)))
                {
                    property.IsNullable = true;
                }

                // ب. منع الـ Cascade Delete لكل العلاقات (Restrict) للحماية من حذف البيانات بالخطأ
                var foreignKeys = entityType.GetForeignKeys();
                foreach (var fk in foreignKeys)
                {
                    fk.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

            // ================= 2. إعدادات خاصة وتصحيحات يدوية =================

            // إضافة الأدمن أوتوماتيكياً (Seeding)
            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = 1,
                Name = "SalmaAdmin",
                Email = "admin01@gmail.com",
                Password = "AQAAAAEAACcQAAAAEBy9Mjk9Z3lR5jL2PqX9H3L0T4M5Z6X7qG9zF9vL2K8W7M5Z6X7",
                CreatedDate = new DateTime(2024, 1, 1)
            });

            // حل مشكلة الـ Cascade Path لجدول الـ Order بشكل خاص (لو لزم الأمر)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Address)
                .WithMany()
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.NoAction);

            // حل رسايل الـ Warnings وتوحيد الـ Decimal
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                        .SelectMany(t => t.GetProperties())
                        .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            // ================= 3. تفعيل ملفات الـ Seed (البيانات الجاهزة) =================
            modelBuilder.ApplyConfiguration(new CategorySeed());
            modelBuilder.ApplyConfiguration(new ProductSeed());

            //// السطر ده اللي كان ناقص عشان المقالات تظهر
            //modelBuilder.ApplyConfiguration(new ArticleSeeder());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }
    }
}