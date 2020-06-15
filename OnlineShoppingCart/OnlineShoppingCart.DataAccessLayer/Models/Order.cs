using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderId { get; set; }
        public float TotalAmount { get; set; }
        public float Discount { get; set; }
        public DateTime OrderDate { get; set; }

        public Customer Customers { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
