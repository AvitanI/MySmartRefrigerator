using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MySmartRefrigerator.Models;
using MySmartRefrigerator.Repositories;
using MySmartRefrigerator.Repositories.Interfaces;

namespace MySmartRefrigerator
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region MongoDB Settings

            services.Configure<ProductsDatabaseSettings>(
                Configuration.GetSection(nameof(ProductsDatabaseSettings)));

            services.AddSingleton<IProductsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ProductsDatabaseSettings>>().Value);

            #endregion

            services.AddScoped<IProductService, ProductService>();

            #region Repositories

            services.AddSingleton<IProductPricesRepository, ProductPricesRepository>();

            services.AddSingleton<IProductRepository, ProductRepository>();

            #endregion

            services.AddControllers();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
