using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderItemId { get; set; }
        public float UnitPrice { get; set; }
        public long Quantity { get; set; }
        public float TotalAmount { get; set; }

        public Order Orders { get; set; }
        public ICollection<OrderItemProduct> OrderItemProducts { get; set; }
    }
}
