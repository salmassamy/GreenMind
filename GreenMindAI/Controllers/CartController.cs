using GreenMind.ServiceAbstraction.DTOs; // تأكدي من إضافة ده
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GreenMindAI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart == null || cart.Items == null)
                return NotFound(new { message = "Cart is empty" });

            var response = new CartDto
            {
                CartItems = cart.Items.Select(i => new CartItemDto
                {
                    Id = i.ProductId,
                    Name = i.Product.Name,
                    Price = i.Product.Price,
                    Img = i.Product.ImageURL,
                    Quantity = i.Quantity
                }).ToList(),

                SubTotal = cart.Items.Sum(i => i.Product.Price * i.Quantity),
                Discount = 100
            };

            response.Total = response.SubTotal - response.Discount > 0
                             ? response.SubTotal - response.Discount
                             : 0;

            return Ok(response);
        }
        // 2. إضافة منتج للسلة 
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int userId, int productId, int quantity)
        {
            // تأكدي إن الـ productId رقم 1 أو 2 عشان تتجنبي الـ Foreign Key Error
            await _cartService.AddItemToCartAsync(userId, productId, quantity);
            return Ok(new { message = "Product added successfully" });
        }

        // باقي الـ Methods (Update, Remove, Clear) زي ما هي تمام
        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int newQuantity)
        {
            await _cartService.UpdateQuantityAsync(cartItemId, newQuantity);
            return Ok(new { message = "Quantity updated" });
        }

        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            await _cartService.RemoveItemFromCartAsync(cartItemId);
            return Ok(new { message = "Item removed" });
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartService.ClearCartAsync(userId);
            return Ok(new { message = "Cart cleared" });
        }
    }
}