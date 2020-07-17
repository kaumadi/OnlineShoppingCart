using Microsoft.IdentityModel.Tokens;
using OnlineShoppingCart.BusinessLayer.Helpers;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace OnlineShoppingCart.BusinessLayer.Services
{
    public class UserService : IUserService
    {
       // const string secret= "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<Customer> _customers = new List<Customer>
        { 
          
            new Customer { CustomerId = 1, FirstName = "Test", LastName = "User",Address="Test", UserName = "test",Email="Test",Contact="Test",Password = "test" }
        };


        private readonly AppSettings _appSettings;
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var customer = _customers.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            // return null if user not found
            if (customer == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(customer);

            return new AuthenticateResponse(customer, token);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customers;
        }


        private string generateJwtToken(Customer customer)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            //  var key = Encoding.ASCII.GetBytes(secret);
              var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, customer.CustomerId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

