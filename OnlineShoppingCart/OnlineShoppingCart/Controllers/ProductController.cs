using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using System;
using System.Threading.Tasks;

namespace OnlineShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class ProductController : Controller
    {
        #region private fields
        private readonly IProductRepository _productRepository;
        #endregion

        #region Constructor
        public ProductController(IProductRepository productRepository)
            {
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(_productRepository)); ;
            }
        #endregion

        #region GetAllProductsAsync  
        /// <summary>
        /// GET: api/product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
            public async Task<IActionResult> GetAllProductsAsync()
            {
                var results = await _productRepository?.GetAllProductsAsync();
                return Ok(results);
            }
        #endregion

        #region GetProductByIdAsync 
        /// <summary>
        /// GET api/<controller>/1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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