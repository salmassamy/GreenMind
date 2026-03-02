using GreenMind.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenMind.Presistance.Data.DataSeed
{
    public class AdminSeed : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasData(
                new Admin
                {
                    Id = 1,
                    Name = "Super Admin",
                    Email = "admin@greenmind.com",
                    Password = "Admin@123",
                  
                    CreatedDate = new DateTime(2026, 3, 1)
                }
            );
        }
    }
}