using OnlineShoppingCart.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShoppingCart.DataAccessLayer.ViewModels
{
    public class OrderDetailsViewModel
    {
            public int OrderId { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalAmount { get; set; }
            public string PaymentMethod { get; set; }
            public int CustomerId { get; set; }
            public string CustomerName { get; set; }
            public string Address { get; set; }
            public string Contact { get; set; }
            public List<OrderItem> orderItems { get; set; }
          
    }
}
