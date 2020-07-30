using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string UserName { get; set; }
        //[JsonIgnore]
        //public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
