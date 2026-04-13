using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;

namespace GreenMind.DataSeed
{
    public static class OrderSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (context.Orders.Any())
                return;

            var user = context.Users.FirstOrDefault();
            var product = context.Products.FirstOrDefault();

            if (user == null || product == null)
                return;

            var orders = new List<Order>
            {
                new Order
                {
                    UserId = user.Id,
                    OrderDate = DateTime.UtcNow.AddDays(-1),
                    TotalAmount = 150,
                    Status = "Pending"
                },
                new Order
                {
                    UserId = user.Id,
                    OrderDate = DateTime.UtcNow.AddHours(-5),
                    TotalAmount = 300,
                    Status = "Completed"
                },
                new Order
                {
                    UserId = user.Id,
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = 450,
                    Status = "Shipped"
                }
            };

            await context.Orders.AddRangeAsync(orders);
            await context.SaveChangesAsync();
        }
    }
}
