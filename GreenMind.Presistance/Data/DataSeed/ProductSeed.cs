using GreenMind.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenMind.Presistance.Data.DataSeed
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Potting Mix",
                    Price = 100,
                    Description = "Organic soil",
                    ImageURL = "soil.jpg",
                    StockQuantity = 50,
                    CategoryId = 1,
                   
                    CreatedDate = new DateTime(2026, 1, 1)
                }
            );
        }
    }
}
