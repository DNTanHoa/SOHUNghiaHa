using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using GleamTech.FileUltimate.AspNet;
using GleamTech.FileUltimate.AspNet.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SOHU.Data.Repositories;
using SOHU.Data.Models;
using Newtonsoft.Json.Serialization;
using GleamTech.AspNet.Core;

namespace NghiaHa.MVC
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseGleamTech();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
