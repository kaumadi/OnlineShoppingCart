using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
