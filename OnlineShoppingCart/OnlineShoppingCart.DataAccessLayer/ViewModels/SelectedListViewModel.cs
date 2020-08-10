using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShoppingCart.DataAccessLayer.ViewModels
{
    public class SelectedListViewModel
    {
        public int ProductId { get; set; }
        public int OrderdQty { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
