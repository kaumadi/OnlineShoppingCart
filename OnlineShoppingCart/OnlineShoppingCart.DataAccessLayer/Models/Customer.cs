using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerId { get; set; }
        [Required()]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [StringLength(12)]
        public string ContactNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Email { get; set; }
        [Required()]
        public string UserName { get; set; }
        [Required()]
        public string Password { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
