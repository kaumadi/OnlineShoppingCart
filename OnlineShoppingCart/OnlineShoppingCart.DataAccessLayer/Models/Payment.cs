using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class Payment
    {
        [ForeignKey("OrderID")]

        public int PaymentId { get; set; }
        public string PaymentType { get; set; }

        public Order Orders { get; set; }
    }
}
