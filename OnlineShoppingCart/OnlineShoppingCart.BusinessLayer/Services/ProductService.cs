using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.DataAccessLayer.Contexts;
using OnlineShoppingCart.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoppingCart.BusinessLayer.Repositories
{
    public class ProductService : IProductRepository
    {
        #region private properties
        readonly OnlineShoppingCartContext _shoppingcartContext;
        #endregion

        #region Constructor
        public ProductService(OnlineShoppingCartContext context)
        {
            _shoppingcartContext = context ?? throw new ArgumentNullException(nameof(context)); ;
        }
        #endregion

        #region GetAllProductsAsync
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _shoppingcartContext.Products.ToListAsync(); 
        }
        #endregion

        #region GetPrductByIdAsync
        public async Task<Product> GetPrductByIdAsync(long item)
        {
            return await _shoppingcartContext.Products.FirstOrDefaultAsync(e => e.ProductId == item);
        }
        #endregion

    }
}
