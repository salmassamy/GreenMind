using GreenMind.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenMind.Presistance.Data.DataSeed
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "Seeds", CreatedDate = new DateTime(2026, 1, 1) },
                new Category { Id = 2, Name = "Soil", CreatedDate = new DateTime(2026, 1, 1) },
                new Category { Id = 3, Name = "Tools", CreatedDate = new DateTime(2026, 1, 1) }
            );
        }
    }
}