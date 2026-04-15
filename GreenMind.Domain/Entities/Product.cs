using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenMind.Domain.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
       public string? Desc { get; set; } = null!;
        public string Img { get; set; } = null!;
        public int StockQuantity { get; set; }

        public string ImageURL { get; set; } = string.Empty;

        
        // 🔥 Foreign Key (Category is int)
        public int CategoryId { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
        // Navigation Property
        public Category? Category { get; set; }
    }
}