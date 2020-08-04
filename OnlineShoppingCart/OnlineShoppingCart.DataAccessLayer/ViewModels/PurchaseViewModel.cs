using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShoppingCart.DataAccessLayer.ViewModels
{
    public class PurchaseViewModel
    {
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public int CustomerId { get; set; }
        public string Token { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal OrderItemTotalPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderId { get; set; }
        public string PaymentType { get; set; }


    }
}
