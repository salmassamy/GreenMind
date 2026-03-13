using System.ComponentModel.DataAnnotations;

namespace GreenMind.ServiceAbstraction.DTOs
{
    public class CropRecommendationDto
    {
        [Required]
        [Range(0, 140)]
        public double Nitrogen { get; set; }

        [Required]
        [Range(0, 120)]
        public double Phosphorus { get; set; }

        [Required]
        [Range(0, 200)]
        public double Potassium { get; set; }

        [Required]
        [Range(5, 8.5)]
        public double PH { get; set; }

        [Required]
        [Range(10, 45)]
        public double Temperature { get; set; }

        [Required]
        [Range(20, 90)]
        public double Humidity { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        [RegularExpression("^(Clay|Sandy|Loamy|Silty)$", ErrorMessage = "Invalid soil type")]
        public string? SoilType { get; set; }
    }
}