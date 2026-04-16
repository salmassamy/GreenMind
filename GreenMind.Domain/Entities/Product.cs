using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenMind.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; } // رجعناه int عادي

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Desc { get; set; } // استخدمي خاصية واحدة للوصف لعدم التكرار

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Img { get; set; } = string.Empty;

        public int StockQuantity { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }

        // Navigation Properties
        public Category? Category { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}