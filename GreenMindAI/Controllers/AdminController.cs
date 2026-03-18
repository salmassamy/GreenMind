using GreenMind.ServiceAbstraction.DTOs;
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

      
       
       

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateUpdateProductDto dto)
        {
            return Ok(await _service.CreateProductAsync(dto));
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateUpdateProductDto dto)
        {
            return Ok(await _service.UpdateProductAsync(id, dto));
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _service.DeleteProductAsync(id);
            return Ok(new { message = "Product deleted successfully" });
        }

        [HttpGet("user-activities")]
        public async Task<IActionResult> GetUserActivities([FromQuery] string? search)
        {
            return Ok(await _service.GetUserActivitiesAsync(search));
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _service.GetOrdersAsync());
        }
    }
}