namespace GreenMind.ServiceAbstraction.DTOs
{
    public class CreateUpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; } 
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}