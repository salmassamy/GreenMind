using System.ComponentModel.DataAnnotations;

namespace GreenMind.ServiceAbstraction.DTOs
{
    public class FertilizerRecommendationDto
    {
        [Required(ErrorMessage = "Crop name is required")]
        public string? Crop { get; set; }

        [Required]
        [Range(0, 140, ErrorMessage = "Nitrogen must be between 0 and 140")]
        public double Nitrogen { get; set; }

        [Required]
        [Range(0, 120, ErrorMessage = "Phosphorus must be between 0 and 120")]
        public double Phosphorus { get; set; }

        [Required]
        [Range(0, 200, ErrorMessage = "Potassium must be between 0 and 200")]
        public double Potassium { get; set; }

        [Required]
        [Range(5, 8.5, ErrorMessage = "PH must be between 5 and 8.5")]
        public double PH { get; set; }

        [Required]
        [Range(10, 45, ErrorMessage = "Temperature must be between 10 and 45")]
        public double Temperature { get; set; }

        [Required]
        [Range(20, 90, ErrorMessage = "Humidity must be between 20 and 90")]
        public double Humidity { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
        public int Month { get; set; }

        // في الـ DTO بتاعك
        [Required]
        [RegularExpression("^(Clay|Sandy|Loamy|Silty)$", ErrorMessage = "Invalid soil type")]
        public string? SoilType { get; set; }
    }
}