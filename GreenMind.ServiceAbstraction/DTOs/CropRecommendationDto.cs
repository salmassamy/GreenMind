using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GreenMind.ServiceAbstraction.DTOs
{
    public class CropRecommendationDto
    {
        [Required]
        [Range(0, 120)] // تعديل المدى بناءً على ريكوايرمنت رحاب
        [JsonPropertyName("N")] // لازم يكون حرف N كبير كما هو مطلوب للموديل
        public double Nitrogen { get; set; }

        [Required]
        [Range(0, 50)] // تعديل المدى بناءً على ريكوايرمنت رحاب
        [JsonPropertyName("P")] // لازم يكون حرف P كبير
        public double Phosphorus { get; set; }

        [Required]
        [Range(0, 200)]
        [JsonPropertyName("K")] // لازم يكون حرف K كبير
        public double Potassium { get; set; }

        [Required]
        [Range(7.0, 8.5)] // الالتزام بالمدى المطلوب لضمان عدم حدوث Error 422
        [JsonPropertyName("ph")]
        public double PH { get; set; }

        [Required]
        [Range(10, 45)] // المدى المطلوب للحرارة
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [Required]
        [Range(20, 80)] // المدى المطلوب للرطوبة
        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }

        [Required]
        [Range(1, 12)]
        [JsonPropertyName("month")]
        public int Month { get; set; }

        [Required]
        // الـ Soil Type لازم يتبعت حروف صغيرة ومن اللستة المحددة
        [JsonPropertyName("soil_type")]
        public string? SoilType { get; set; }

        [Required]
        [JsonPropertyName("governorate")]
        public string Governorate { get; set; } = string.Empty;
    }
}