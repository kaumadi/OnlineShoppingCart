using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShoppingCart.DataAccessLayer.ViewModels
{
    public class ProductStockStatus
    {
        public int ProductID { get; set; }
        public bool ProductCurrentStatus { get; set; }
    }
}
