using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GleamTech.AspNet.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SOHU.API.ServiceExtensions;
using SOHU.Data.Repositories;
using SOHU.Data.Models;

namespace SOHU.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddGleamTech();
            services.AddTokenAuthentication(Configuration);

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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseGleamTech();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
