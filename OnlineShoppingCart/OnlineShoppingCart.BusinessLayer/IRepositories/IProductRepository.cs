using OnlineShoppingCart.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingCart.BusinessLayer.IRepositories
{
    public interface IProductRepository<TEntity>
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetAsync(long item);


    }
}
