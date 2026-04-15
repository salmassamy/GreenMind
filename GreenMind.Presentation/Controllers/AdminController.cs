using GreenMind.Domain.Entities;
using GreenMind.ServiceAbstraction.DTOs;
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenMindAI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminDashboardService _service;

        public AdminController(IAdminDashboardService service)
        {
            _service = service;
        }

        // ================= CREATE =================
        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateUpdateProductDto dto)
        {
            return Ok(await _service.CreateProductAsync(dto));
        }

        // ================= UPDATE =================
        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromForm] CreateUpdateProductDto dto)
        {
            var guidId = Guid.Parse(id); // 🔥 تحويل
            var result = await _service.UpdateProductAsync(guidId, dto);
            return Ok(result);
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var guidId = Guid.Parse(id); // 🔥 تحويل
            await _service.DeleteProductAsync(guidId);
            return Ok(new { message = "Product deleted successfully" });
        }

        // ================= GET ALL =================
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _service.GetProductsAsync();
            return Ok(result);
        }

        // ================= USER ACTIVITIES =================
        [HttpGet("user-activities")]
        public async Task<IActionResult> GetUserActivities([FromQuery] string? search)
        {
            return Ok(await _service.GetUserActivitiesAsync(search));
        }

        // ================= ORDERS =================
        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _service.GetOrdersAsync());
        }
     
 

        // ================= HOME SUMMARY =================
        [HttpGet("home-summary")]
        public async Task<IActionResult> GetHomeSummary()
        {
            var result = await _service.GetHomeSummaryAsync();
            return Ok(result);
        }
    }
}