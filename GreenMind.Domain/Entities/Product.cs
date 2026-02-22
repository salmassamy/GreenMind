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
        public string Description { get; set; } = null!;
        public string ImageURL { get; set; } = null!;
        public int StockQuantity { get; set; }

        // Foreign Key للصنف
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        // الربط مع CartItem (المنتج ممكن يظهر في كذا عربة تسوق)
        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}
