using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class OrderItemProduct
    {
        [Key]
        public int OrderItemProductID { get; set; }
        public int ProductID { get; set; }
        public Product Products { get; set; }
        public int OrderItemID { get; set; }
        public OrderItem OrderItems { get; set; }
    }
}
