using GreenMind.Domain.Entities;
using GreenMind.ServiceAbstraction.DTOs;
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GreenMind.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize] 
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(CheckoutRequestDto checkoutDto)
        {
            // 1. عطلنا الجزء اللي بيسأل على التوكن واللوجن مؤقتاً
            // var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            // if (userIdClaim == null)
            //    return Unauthorized();

            // 2. ثبتنا الـ ID برقم يوزر موجود عندك في الداتا بيز (وليكن 6)
            int userId = 6;

            try
            {
                // 3. الميثود هتشتغل عادي بالـ ID رقم 6 
                var orderId = await _orderService.PlaceOrderAsync(userId, checkoutDto);

                return Ok(new
                {
                    message = "Order placed successfully!",
                    orderId = orderId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var orders = await _orderService.GetUserOrdersAsync(userId);

            return Ok(orders);
        }
    }
}
