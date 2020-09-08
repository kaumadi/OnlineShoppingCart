using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShoppingCart.BusinessLayer.Helpers;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.DataAccessLayer.Models;
using OnlineShoppingCart.DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingCart.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        #region Private Members
        private IUserService _userService;
        private IMapper _mapper;
        const string secret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";
        #endregion

        #region Constractor
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        #endregion

        #region User Login Authentication
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticationViewModel model)
        {
            var customer = _userService.Authenticate(model.Username, model.Password);

            if (customer == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
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
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Address = customer.Address,
            Email = customer.Email,
            Contact = customer.Contact,
            Username = customer.UserName,
            Token = tokenString
            });
        }
        #endregion

        #region New User Registration
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterViewModel model)
        {
            // map model to entity
            var customer = _mapper.Map<Customer>(model);

            try
            {
                // create user
               var result= _userService.Create(customer, model.Password);
                return Ok(result);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
        #endregion

        //#region Get All Users
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var users = await  _userService.GetAll();
        //    var model = _mapper.Map<IList<CustomerViewModel>>(users);
        //    return Ok(users);

        //}
        //#endregion

        //#region Get User By Id
        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    var user = _userService.GetById(id);
        //    var model = _mapper.Map<CustomerViewModel>(user);
        //    return Ok(model);
        //}
        //#endregion
    }
}
