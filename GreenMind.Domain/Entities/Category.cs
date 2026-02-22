using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenMind.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;

        // العلاقة: الصنف الواحد يحتوي على منتجات كتير
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
