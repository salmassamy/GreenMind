using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.ServiceAbstraction.DTOs;
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenMind.Service.Services.ShoppingCart
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetUserOrdersAsync(int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate) 
                .Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status
                })
                .ToListAsync();

            return orders;
        }
        public async Task<int> PlaceOrderAsync(int userId, CheckoutRequestDto checkoutDto)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.Items.Any())
                throw new Exception("The basket is empty, add the first products!");

            var subTotal = cart.Items.Sum(item => item.Quantity * item.Product.Price);

            var discount = checkoutDto.CartDetails.Discount;

            var shipping = 0m;
            var taxes = 0m;
            var total = subTotal - discount + shipping + taxes;

            var address = new Address
            {
                City = checkoutDto.CustomerDetails.City,    // عدليها كده
                Street = checkoutDto.CustomerDetails.Address, // عدليها كده
                Phone = checkoutDto.CustomerDetails.Phone,    // عدليها كده
                Notes = checkoutDto.CustomerDetails.Notes,    // عدليها كده
                UserId = userId
            };
            _context.Addresses.Add(address);
            var order = new Order
            {
                UserId = userId, // ضيفي دي
                OrderDate = DateTime.UtcNow, // ضيفي دي
                Status = "Pending", // ضيفي دي
                SubTotal = checkoutDto.CartDetails.SubTotal,
                DiscountAmount = checkoutDto.CartDetails.Discount,
                ShippingCost = checkoutDto.CartDetails.Shipping,
                TaxAmount = checkoutDto.CartDetails.Taxes,
                TotalAmount = checkoutDto.CartDetails.Total,
                PaymentMethod = checkoutDto.PaymentMethod, // ضيفي دي
                Phone = checkoutDto.CustomerDetails.Phone,
                Notes = checkoutDto.CustomerDetails.Notes
            };

            foreach (var item in cart.Items)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                });
            }

            _context.Orders.Add(order);

            order.Address = address;

            _context.CartItems.RemoveRange(cart.Items);
            await _context.SaveChangesAsync();

            return order.Id;
        }
    }
}