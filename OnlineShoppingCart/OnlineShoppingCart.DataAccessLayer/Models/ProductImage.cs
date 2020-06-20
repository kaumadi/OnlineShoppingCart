using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public Product Products { get; set; }
    }
}
