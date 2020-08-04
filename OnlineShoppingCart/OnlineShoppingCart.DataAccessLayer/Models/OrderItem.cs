using System.Collections.Generic;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public Order Orders { get; set; }
        public ICollection<OrderItemProduct> OrderItemProducts { get; set; }
    }


}
