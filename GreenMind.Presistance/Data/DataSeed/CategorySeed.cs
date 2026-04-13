using GreenMind.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenMind.Presistance.DataSeed
{
    public static class CategorySeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
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