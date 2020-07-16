using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }  // navigation property
        public string Address { get; set; }
      
        public ICollection<Order> Orders { get; set; }
    }
}
