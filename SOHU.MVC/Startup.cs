using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GleamTech.AspNet.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using GleamTech.FileUltimate.AspNet;
using GleamTech.FileUltimate.AspNet.UI;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace SOHU.MVC
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
            services.AddControllers(options => options.EnableEndpointRouting = false)
                    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddDbContext<SOHUContext>();

            services.AddTransient<IMembershipRepository, MembershipRepository>();
            services.AddTransient<IProductConfigRepository, ProductConfigRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IConfigRepository, ConfigRepository>();
            services.AddTransient<IInvoicePaymentRepository, InvoicePaymentRepository>();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IInvoiceDetailRepository, InvoiceDetailRepository>();
            services.AddTransient<IMembershipPaymentRepository, MembershipPaymentRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartDetailRepository, CartDetailRepository>();

            services.AddControllersWithViews();

            services.AddGleamTech();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseGleamTech();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "ProductDetail",
                    pattern: "{MetaTitle}-{ProductID}.html",
                    defaults: new { controller = "Home", action = "ProductDetail" });

                endpoints.MapControllerRoute(
                    name: "About",
                    pattern: "about.html",
                    defaults: new { controller = "Home", action = "About" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
