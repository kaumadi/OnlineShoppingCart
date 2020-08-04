using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public DateTime OrderDate { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customers { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
