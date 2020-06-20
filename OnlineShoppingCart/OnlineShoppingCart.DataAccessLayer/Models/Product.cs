using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }
        [Required()]
        public string ProductName { get; set; }   
        public float UnitPrice { get; set; }
        public long UnitsInStock { get; set; }
        public string Description { get; set; }

        public Category Categories { get; set; }

        public ICollection<OrderItemProduct> OrderItemProducts { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
