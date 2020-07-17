using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShoppingCart.DataAccessLayer.Models
{
    public class AuthenticateResponse
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(Customer customer, string token)
        {
            CustomerId = customer.CustomerId;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Address = customer.Address;
            Email = customer.Email;
            Contact = customer.Contact;
            Username = customer.UserName;
            Token = token;
        }
    }
}
