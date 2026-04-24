using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.ServiceAbstraction.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;


namespace GreenMind.Presentation.Controllers
{
    [ApiController]

    [Route("api")]
    public class AIController : ControllerBase
    {

        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context; 

        public AIController(HttpClient httpClient, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        [HttpPost("recommend-crop")]
        public async Task<IActionResult> GetCropRecommendation([FromBody] CropRecommendationDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string aiApiUrl = "https://overplant-growing-handmade.ngrok-free.dev/predict";

                var response = await _httpClient.PostAsJsonAsync(aiApiUrl, input);

                if (!response.IsSuccessStatusCode)
                {
                   
                    var aiErrorMessage = await response.Content.ReadAsStringAsync();

                    return BadRequest(new
                    {
                        message = "AI Validation Error",
                        details = aiErrorMessage
                    });
                }
               
                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                string aiMessage = result.GetProperty("message").GetString() ?? "Unknown Crop Result";

                var history = new UserActivityHistory
                {
                    UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "6"),
                    Type = "crop",
                    Date = DateTime.Now.ToString("yyyy-MM-dd"),
                    Text = aiMessage,
                    Image = $"{Request.Scheme}://{Request.Host}/uploads/crop_default.png"
                };

                _context.UserActivityHistory.Add(history);
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost("recommend-fertilizer")]
        public async Task<IActionResult> GetFertilizerRecommendation([FromBody] FertilizerRecommendationDto input)

        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await Task.Yield();

                var mockResult = new { recommendedFertilizer = "Urea" };
                return Ok(mockResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost("detect-disease")]
        public async Task<IActionResult> DetectDisease([FromForm] DiseaseDiagnosisDto input)
        {
            if (input.Images == null || !input.Images.Any())
            {
                return BadRequest(new { message = "No images uploaded." });
            }

            var finalResults = new List<object>();
            string aiServerUrl = "http://127.0.0.1:8080/predict";

            try
            {
                foreach (var file in input.Images)
                {
                    // 1. حفظ الصورة فعلياً على السيرفر عشان محمد يشوفها
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);

                    var fullPath = Path.Combine(uploadsPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // 2. إنشاء رابط الصورة الحقيقي
                    // بدل ما نثبت localhost، نخليه ياخد العنوان اللي الطلب جاي منه (سواء ngrok أو غيره)
                    var imageUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";

                    // 3. كلام سيرفر الـ AI (المنطق بتاعك زي ما هو)
                    using var content = new MultipartFormDataContent();
                    using var fileStream = file.OpenReadStream();
                    var fileContent = new StreamContent(fileStream);
                    content.Add(fileContent, "image", file.FileName);

                    var response = await _httpClient.PostAsync(aiServerUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var aiData = await response.Content.ReadFromJsonAsync<AIDetectionResponse>();
                        if (aiData != null)
                        {
                            var history = new UserActivityHistory
                            {
                                UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "6"),
                                Type = "disease",
                                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                                Image = imageUrl, // الرابط الحقيقي اللي هيظهر الصورة لمحمد
                                Text = $"Plant: {aiData.Plant}, Disease: {aiData.Disease}, Severity: {aiData.Severity}"
                            };

                            _context.UserActivityHistory.Add(history);
                            finalResults.Add(new { imageName = file.FileName, diagnosis = aiData, permanentImageUrl = imageUrl });
                        }
                    }
                    else
                    {
                        var errorReason = await response.Content.ReadAsStringAsync();
                        finalResults.Add(new { imageName = file.FileName, error = "AI Server Error", detail = errorReason });
                    }
                }

                await _context.SaveChangesAsync();
                return Ok(new { results = finalResults });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
        [HttpGet("user-ai-history/{type}")]
         public async Task<IActionResult> GetUserHistory(string type)
        {
            try
            {
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "6";
                int userId = int.Parse(userIdStr);

                var history = await _context.UserActivityHistory
                    .Where(h => h.UserId == userId && h.Type == type) // فلترة بالـ UserId والـ Type مع بعض
                    .OrderByDescending(h => h.Id)
                    .Select(h => new {
                        h.Id,
                        h.Text,
                        h.Date,
                        h.Image
                    })
                    .ToListAsync();

                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
    }