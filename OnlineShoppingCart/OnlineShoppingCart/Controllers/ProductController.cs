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
        #region private fields
        private readonly IProductRepository<Product> _productRepository;
        #endregion

        #region Contructor
        public ProductController(IProductRepository<Product> productRepository)
            {
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(_productRepository)); ;
            }
        #endregion

        #region GetAllProductsAsync 
        // GET: api/product
        [HttpGet]
            public async Task<IActionResult> GetAllProductsAsync()
            {
                var results = await _productRepository?.GetAllProductsAsync();
                return Ok(results);
            }
        #endregion

        #region GetProductByIdAsync 
        // GET api/<controller>/1
        [HttpGet("{id}", Name = "GetProductByIdAsync")]
            public async Task<IActionResult> GetProductByIdAsync(long id)
            {
                var products = await _productRepository?.GetPrductByIdAsync(id);

                if (products == null)
                {
                    ModelState.AddModelError("id", "Provided Id cannot be found");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(products);
            }
        #endregion

    }
}