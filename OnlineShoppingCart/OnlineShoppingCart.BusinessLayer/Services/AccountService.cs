using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.DataAccessLayer.Contexts;
using OnlineShoppingCart.DataAccessLayer.Models;
using System;
using System.Threading.Tasks;

namespace OnlineShoppingCart.BusinessLayer.Services
{
    public class AccountService : IAccountRepository
    {
        readonly OnlineShoppingCartContext _shoppingcartContext;
        

        public AccountService(OnlineShoppingCartContext context)
        {
            _shoppingcartContext = context ?? throw new ArgumentNullException(nameof(context)); 
            
        }
   

        public async Task<IActionResult> AddNewUser(RegistrationViewModel model,AppUser userIdentity)
        {
                   
            await _shoppingcartContext.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Address = model.Address });
            await _shoppingcartContext.SaveChangesAsync();

            return new OkResult();
        }

    }
}
