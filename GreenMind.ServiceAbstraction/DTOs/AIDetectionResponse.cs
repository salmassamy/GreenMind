namespace GreenMind.ServiceAbstraction.DTOs
{
    public class AIDetectionResponse
    {
        public string? Plant { get; set; }
        public string? Disease { get; set; }
        public string? Severity { get; set; }
        public string? Pathogen_type { get; set; }
        public List<string>? Symptoms { get; set; } = [];
        public List<string>? Prevention { get; set; } = [];
        public List<string>? Treatment { get; set; } = [];
        public List<string>? care_tips { get; set; } = [];
    }
}