namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class OrderItemProduct
    {
        public int OrderItemProductId { get; set; }
        public Product Products { get; set; }
        public OrderItem OrderItems { get; set; }
    }
}
