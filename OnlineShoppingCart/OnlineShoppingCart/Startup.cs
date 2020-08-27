using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OnlineShoppingCart.BusinessLayer.Helpers;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.BusinessLayer.Repositories;
using OnlineShoppingCart.BusinessLayer.Services;
using OnlineShoppingCart.DataAccessLayer.Contexts;
using OnlineShoppingCart.DataAccessLayer.Models;
using OnlineShoppingCart.DataAccessLayer.ViewModels;
using System;
using System.IO;
using System.Text;

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
            services.AddScoped<IOrderService, OrderServices>();


            services.AddControllers();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });


            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //services.AddAuthentication(HttpSysDefaults.AuthenticationScheme);

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();

            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            //services.AddSingleton(mapper);
            // services.AddScoped<IAccountRepository<RegistrationViewModel>, AccountService>();
            //services.AddIdentity<AppUser, IdentityRole>
            //   (opt =>
            //   {
            //               // configure identity options
            //       opt.Password.RequireDigit = false;
            //       opt.Password.RequireLowercase = false;
            //       opt.Password.RequireUppercase = false;
            //       opt.Password.RequireNonAlphanumeric = false;
            //       opt.Password.RequiredLength = 6;
            //     //  opt.User.RequireUniqueEmail = true;
            //   })
            //   .AddEntityFrameworkStores<OnlineShoppingCartContext>()
            //   .AddDefaultTokenProviders();


            // services.AddAutoMapper(typeof(Startup));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMvc();
          

            
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new
                PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Templete")),
                RequestPath = new PathString("/Templete")
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();    
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
     
    }
}
