using System.Collections.Generic;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }   
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Category Categories { get; set; }
        public ICollection<OrderItemProduct> OrderItemProducts { get; set; }

        
    }
}
