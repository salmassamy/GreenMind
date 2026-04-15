using GreenMind.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenMind.Presistance.Data.DataSeed
namespace GreenMind.Presistance.DataSeed
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    public static class CategorySeed
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        public static void Seed(ModelBuilder modelBuilder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "Seeds", CreatedDate = new DateTime(2026, 1, 1) },
                new Category { Id = 2, Name = "Soil", CreatedDate = new DateTime(2026, 1, 1) },
                new Category { Id = 3, Name = "Tools", CreatedDate = new DateTime(2026, 1, 1) }
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Fertilizers"
                },
                new Category
                {
                    Id = 2,
                    Name = "Seeds"
                },
                new Category
                {
                    Id = 3,
                    Name = "Tools"
                }
            );
        }
    }
}