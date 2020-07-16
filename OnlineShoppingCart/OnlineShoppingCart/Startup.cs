using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.BusinessLayer.Repositories;
using OnlineShoppingCart.BusinessLayer.Services;
using OnlineShoppingCart.DataAccessLayer.Contexts;
using OnlineShoppingCart.DataAccessLayer.Mappings;
using OnlineShoppingCart.DataAccessLayer.Models;
using OnlineShoppingCart.DataAccessLayer.Validations;
using System;

namespace OnlineShoppingCart
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OnlineShoppingCartContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OnlineShoppingCartContext"), b => b.MigrationsAssembly("OnlineShoppingCart")));
            services.AddScoped<IProductRepository, ProductService>();
            services.AddScoped<IAccountRepository, AccountService>();


            //services.AddSingleton(mapper);
            // services.AddScoped<IAccountRepository<RegistrationViewModel>, AccountService>();
            services.AddIdentity<AppUser, IdentityRole>
               (opt =>
               {
                           // configure identity options
                   opt.Password.RequireDigit = false;
                   opt.Password.RequireLowercase = false;
                   opt.Password.RequireUppercase = false;
                   opt.Password.RequireNonAlphanumeric = false;
                   opt.Password.RequiredLength = 6;
                 //  opt.User.RequireUniqueEmail = true;
               })
               .AddEntityFrameworkStores<OnlineShoppingCartContext>()
               .AddDefaultTokenProviders();


            


            services.AddControllers();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            // services.AddAutoMapper(typeof(Startup));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMvc()
  .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegistrationViewModelValidator>());
            //services.AddCors(o => o.AddPolicy("CorePolicy", builder =>
            //{
            //    builder.AllowAnyMethod();
            //}));
            
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            //app.UseCors("CorePolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseRouting();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapGet("/", async context =>
        //        {
        //            await context.Response.WriteAsync("Hello World!");
        //        });
        //    });
        //}
    }
}
