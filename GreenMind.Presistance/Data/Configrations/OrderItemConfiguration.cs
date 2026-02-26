using GreenMind.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenMind.Presistance.Data.Configrations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // حل تحذير الـ UnitPrice
            builder.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
        }
    }
}