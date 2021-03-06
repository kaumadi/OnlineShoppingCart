﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShoppingCart.DataAccessLayer.ViewModels
{
    public class ProductStockStatus
    {
        public int ProductId { get; set; }
        public int OrderdQty { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public bool ProductCurrentStatus { get; set; }
    }
}
