using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenMind.Domain.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; 
        public decimal SubTotal { get; set; }     
        public decimal DiscountAmount { get; set; } 
        public decimal ShippingCost { get; set; }   
        public decimal TaxAmount { get; set; }       
        public decimal TotalAmount { get; set; }   
    
        public string PaymentMethod { get; set; } = "Cash on delivery";
        public string Phone { get; set; } = null!;
        public string? Notes { get; set; }
        // العلاقات (Relationships)
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int AddressId { get; set; } 
        public Address Address { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
