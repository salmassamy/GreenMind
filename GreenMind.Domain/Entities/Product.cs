using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenMind.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
       public string? Desc { get; set; } = null!;
        public string Img { get; set; } = null!;
        public int StockQuantity { get; set; }

        
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}
