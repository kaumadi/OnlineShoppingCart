using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingCart.DataAccessLayer.ViewModels
{
    public class AuthenticationViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
