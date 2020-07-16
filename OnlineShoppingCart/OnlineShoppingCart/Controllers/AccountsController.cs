

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.DataAccessLayer.Contexts;
using OnlineShoppingCart.DataAccessLayer.Models;

using System;
using System.Threading.Tasks;

namespace OnlineShoppingCart.Controllers
{

        [Route("api/[controller]")]
    public class AccountsController : Controller
        {

            private readonly IAccountRepository _accountRepository;
            private readonly IMapper _mapper;
            private readonly UserManager<AppUser> _userManager;

        public AccountsController(IAccountRepository accountRepository, IMapper mapper, UserManager<AppUser> userManager)
            {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

        }

        // POST api/accounts
        [HttpPost]
            public async Task<IActionResult> NewCustomerRegistration([FromBody]RegistrationViewModel model)
            {
            
            if (model == null)
                {
                    return BadRequest();
                }
            if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            var userIdentity = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
            {

                return new BadRequestObjectResult("Bad Request Object Result");
            }
            
            await _accountRepository?.AddNewUser(model, userIdentity);
           
            return new OkObjectResult("New User Registerd Successfully");
            
        }
    }

    }
