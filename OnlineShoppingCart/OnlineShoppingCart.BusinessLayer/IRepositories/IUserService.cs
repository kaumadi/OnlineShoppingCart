using OnlineShoppingCart.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoppingCart.BusinessLayer.IRepositories
{
    public interface IUserService
    {
        //AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<IEnumerable<Customer>> GetAll();
        Customer Create(Customer customer, string password);
        Customer GetById(int CustomerId);
        Customer Authenticate(string username, string password);
    }
}
