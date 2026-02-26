using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        // 1. عرض السلة بالكامل (عشان الجدول اللي في الصورة)
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null) return NotFound(new { message = "Cart is empty" });
            return Ok(cart);
        }

        // 2. إضافة منتج للسلة (من صفحة المنتجات)
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int userId, int productId, int quantity)
        {
            await _cartService.AddItemToCartAsync(userId, productId, quantity);
            return Ok(new { message = "Product added successfully" });
        }

        // 3. تحديث الكمية (أزرار + و - في الـ UI)
        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int newQuantity)
        {
            await _cartService.UpdateQuantityAsync(cartItemId, newQuantity);
            return Ok(new { message = "Quantity updated" });
        }

        // 4. حذف منتج معين (علامة الـ X)
        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            await _cartService.RemoveItemFromCartAsync(cartItemId);
            return Ok(new { message = "Item removed" });
        }

        // 5. مسح السلة بالكامل (زرار Clear Shopping Cart)
        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartService.ClearCartAsync(userId);
            return Ok(new { message = "Cart cleared" });
        }
    }
}