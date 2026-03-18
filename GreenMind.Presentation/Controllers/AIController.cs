using GreenMind.ServiceAbstraction.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace GreenMind.Presentation.Controllers
{
    [ApiController]
   
    [Route("api")]
    public class AIController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public AIController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("recommend-crop")]
        public async Task<IActionResult> GetCropRecommendation([FromBody] CropRecommendationDto input)
        {
            if (!ModelState.IsValid)
            {
                //مؤقتا بس عشان التحذير يروح لما نستلم ال api الحقيقي من تيم الـ AI، بعدين لما تشتغل مع الرابط الحقيقي هتشيل السطر ده
                await Task.CompletedTask;
                return BadRequest(ModelState);
            }

            try
            {
                // حالياً سنترك هذا الجزء معطلاً أو يرجع Mock Data حتى يسلمك تيم الـ AI الرابط الحقيقي
                // string aiApiUrl = "http://127.0.0.1:5000/predict"; 
                // var response = await _httpClient.PostAsJsonAsync(aiApiUrl, input);

                // الرد الوهمي (Mock Response) متوافق مع اسم الخانة اللي الفرونت مستنيها
                var mockResult = new { recommendedCrop = "Rice" };

                return Ok(mockResult);

                /* الكود الحقيقي عند توفر رابط تيم الـ AI:
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<object>();
                    return Ok(result);
                }
                return BadRequest("مشكلة في التواصل مع سيرفر الـ AI");
                */
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
    }
}