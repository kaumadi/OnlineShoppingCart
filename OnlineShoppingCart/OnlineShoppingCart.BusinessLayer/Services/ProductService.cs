using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.DataAccessLayer.Contexts;
using OnlineShoppingCart.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingCart.BusinessLayer.Repositories
{
    public class ProductService : IProductRepository<Product>
    {
        readonly OnlineShoppingCartContext _shoppingcartContext;

        public ProductService(OnlineShoppingCartContext context)
        {
            _shoppingcartContext = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _shoppingcartContext.Products.ToListAsync();
          
        }

        public async Task<Product> GetAsync(long item)
        {
            return await _shoppingcartContext.Products.FirstOrDefaultAsync(e => e.ProductId == item);
        
        }
        
    }
}
