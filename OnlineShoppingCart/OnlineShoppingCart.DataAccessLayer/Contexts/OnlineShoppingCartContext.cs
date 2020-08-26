using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.DataAccessLayer.Models;
using System.Collections.Generic;

namespace OnlineShoppingCart.DataAccessLayer.Contexts
{
  
        public class OnlineShoppingCartContext : DbContext
    {
            public OnlineShoppingCartContext(DbContextOptions options)
                  : base(options)
            {
            }

         
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderItem> OrderItems { get; set; }
            public DbSet<Payment> Payments { get; set; }
            public DbSet<Payment> ProductImage { get; set; }
            public DbSet<OrderItemProduct> OrderItemProducts { get; set; }


    }
    
}
