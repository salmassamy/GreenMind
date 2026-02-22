using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenMind.Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public int Quantity { get; set; }

        // ربط الـ CartItem بالعربة (Cart)
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        // ربط الـ CartItem بالمنتج (Product)
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
