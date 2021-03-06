using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;
using WebAPI.Repositories.Services;
using WebAPI.Services;
using WebAPI.Services.Interfaces;

namespace WebAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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

            #region Services

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IStoreService, StoreService>();

            #endregion

            #region Repositories

            services.AddSingleton<IProductPricesRepository, ProductPricesRepository>();

            services.AddSingleton<IProductRepository, ProductRepository>();

            services.AddSingleton<IStoreRepository, StoresRepository>();

            #endregion

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:62934", "https://localhost:44376").AllowAnyHeader()
                                .AllowAnyMethod(); ;
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
