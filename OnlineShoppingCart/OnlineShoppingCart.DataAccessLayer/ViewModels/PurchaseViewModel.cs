using System;
using System.Collections.Generic;

namespace OnlineShoppingCart.DataAccessLayer.ViewModels
{
    public class PurchaseViewModel
    {
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public string Token { get; set; }        
        public List<SelectedListViewModel> selectedListViewModel { get; set; }
        public string PaymentType { get; set; }
    }
}
