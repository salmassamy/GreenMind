using GreenMind.ServiceAbstraction.DTOs;
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Text.RegularExpressions;

namespace GreenMind.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewsController(IReviewService service)
        {
            _service = service;
        }

        // POST /api/reviews
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto dto)
        {
            // ❌ Null check
            if (dto == null)
                return BadRequest("Invalid request body");

            // ✅ Empty / whitespace validation
            if (string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.Phone) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Message))
            {
                return BadRequest("Name, Phone, Email and Message are required");
            }
            // ❌ Email format validation
            var emailRegex = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            if (!Regex.IsMatch(dto.Email, emailRegex))
            {
                return BadRequest("Invalid email format");
            }

            try
            {
                await _service.AddReviewAsync(dto);

                return StatusCode(201, new
                {
                    message = "Review created successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // GET /api/reviews?limit=3
        [HttpGet]
        public async Task<IActionResult> GetReviews([FromQuery] int limit = 3)
        {
            // ❌ حماية من قيم غلط
            if (limit <= 0)
                limit = 3;

            var result = await _service.GetReviewsAsync(limit);

            return Ok(result);
        }
    }
}