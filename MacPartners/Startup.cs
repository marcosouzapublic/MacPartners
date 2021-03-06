using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Repositories;
using MacPartners.Infra.Context;
using MacPartners.Infra.Repositories;
using MacPartners.Infra.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "MacPartnerSession";
                options.LoginPath = "/Account/Login";
                options.SlidingExpiration = true;
            });

            services.AddControllersWithViews();

            services.AddDbContext<MacPartnersContext>(
                options => 
                options
                .UseSqlServer("name = ConnectionStrings:SQLServerContext")
                .UseLazyLoadingProxies()
            );

            services.AddScoped<ICrypter, CrypterService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPartnerRepository, PartnerRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmail, EmailService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<IISOptions>(options => {});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUserRepository userRepository, ICrypter crypter)
        {
            UserService.CheckSuperUser(userRepository, crypter);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
