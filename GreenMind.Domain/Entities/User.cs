using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GreenMind.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
     
        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
        public Cart Cart { get; set; } = null!;
    }
}
