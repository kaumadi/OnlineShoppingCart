using OnlineShoppingCart.DataAccessLayer.Models;
using System.Collections.Generic;

namespace OnlineShoppingCart.BusinessLayer.IRepositories
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<Customer> GetAll();
    }
}
