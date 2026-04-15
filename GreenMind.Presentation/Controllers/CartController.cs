using GreenMind.ServiceAbstraction.DTOs; // تأكدي من إضافة ده
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
                    Img = i.Product.Img,
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

        [HttpPost("add")]
        // [Authorize] 
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto request) // هنا التغيير
        {
            // 1. الجزء بتاع الـ Claim معمول له Comment زي ما هو
            /* var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);
            */

            // 2. هنستخدم userId ثابت (1) للتجربة زي ما إنتي عاملة
            int userId = 1;

            // 3. بنسحب البيانات من الـ request DTO اللي جاي من الفرونت إند
            await _cartService.AddItemToCartAsync(userId, request.ProductId, request.Quantity);

            return Ok(new { message = "Product added successfully to your account's cart!" });
        }

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

        [HttpDelete("clear")]
        // [Authorize]
        public async Task<IActionResult> ClearCart()
        {
            // 1. تعليق مؤقت للجزء بتاع الـ Claim عشان رحاب تمسح الكارت من غير Token
            /* var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);
            */

            // 2. تثبيت الـ userId على رقم 1 للتجربة
            int userId = 1;

            await _cartService.ClearCartAsync(userId);

            return Ok(new { message = "Cart cleared successfully" });
        }
    }
}