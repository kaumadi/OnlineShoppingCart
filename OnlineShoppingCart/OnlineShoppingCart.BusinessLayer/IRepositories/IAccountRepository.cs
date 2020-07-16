using Microsoft.AspNetCore.Mvc;
using OnlineShoppingCart.DataAccessLayer.Models;
using System.Threading.Tasks;

namespace OnlineShoppingCart.BusinessLayer.IRepositories
{
    public interface IAccountRepository
    {
        Task<IActionResult> AddNewUser(RegistrationViewModel model, AppUser userIdentity);
    }
}
