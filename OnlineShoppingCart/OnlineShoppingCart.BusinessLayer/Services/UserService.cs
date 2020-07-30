using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.BusinessLayer.Helpers;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.DataAccessLayer.Contexts;
using OnlineShoppingCart.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingCart.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        const string secret= "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";
        readonly OnlineShoppingCartContext _shoppingcartContext;
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
      

        public UserService(OnlineShoppingCartContext context)
        {
            _shoppingcartContext = context ?? throw new ArgumentNullException(nameof(context));
        }


        //_customers=(from Customer in  _shoppingcartContext.Customer select *).ToList();
        // private readonly AppSettings _appSettings;
        //public AuthenticateResponse Authenticate(AuthenticateRequest model)
        //{
        //    var customer = _shoppingcartContext.Customers.SingleOrDefault(x => x.UserName == model.Username); //&& x.Password == model.Password);

        //    // return null if user not found
        //    if (customer == null) return null;

        //    // authentication successful so generate jwt token
        //    var token = generateJwtToken(customer);

        //    return new AuthenticateResponse(customer, token);
        //}
        public Customer Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var customer = _shoppingcartContext.Customers.SingleOrDefault(x => x.UserName == username);

            // check if username exists
            if (customer == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, customer.PasswordHash, customer.PasswordSalt))
                return null;

            // authentication successful
            return customer;
        }
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _shoppingcartContext.Customers.ToListAsync(); 
        }


        //private string generateJwtToken(Customer customer)
        //{
        //    // generate token that is valid for 7 days
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //      var key = Encoding.ASCII.GetBytes(secret);
        //     //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, customer.CustomerId.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        public Customer Create(Customer customer, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_shoppingcartContext.Customers.Any(x => x.UserName == customer.UserName))
                throw new AppException("Username \"" + customer.UserName + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;

            _shoppingcartContext.Customers.Add(customer);
            _shoppingcartContext.SaveChanges();

            return customer;
        }
        public Customer GetById(int CustomerId)
        {
            return _shoppingcartContext.Customers.Find(CustomerId);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    
}
}

