using System;
using System.Collections.Generic;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public DateTime OrderDate { get; set; }

        public Customer Customers { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
