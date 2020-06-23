using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class ProductController : Controller
    {
     
            private readonly IProductRepository<Product> _productRepository;

            public ProductController(IProductRepository<Product> productRepository)
            {
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(_productRepository)); ;
            }

            // GET: api/product
            [HttpGet]
            public async Task<IActionResult> GetAllAsync()
            {
                var results = await _productRepository?.GetAllAsync();
                return Ok(results);
            }

            // GET api/<controller>/1
            [HttpGet("{id}", Name = "GetOneProductAsync")]
            public async Task<IActionResult> GetOneProductAsync(long id)
            {
                var products = await _productRepository?.GetAsync(id);

                if (products == null)
                {
                    ModelState.AddModelError("id", "provided id cannot be found");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(products);
            }

    }
    }