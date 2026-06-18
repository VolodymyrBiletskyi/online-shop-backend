using api.Modules.ProductModule.Domain;
using api.Modules.ProductModule.Repository;

namespace api.Modules.ProductModule
{
    public static class ProductModuleExtensions
    {
        public static IServiceCollection AddProductModule(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductValidator, ProductValidator>();

            return services;
        }
    }
}
