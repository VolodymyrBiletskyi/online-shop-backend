using api.Modules.CartModule.Domain;
using api.Modules.CartModule.Repository;

namespace api.Modules.CartModule
{
    public static class CartModuleExtensions
    {
        public static IServiceCollection AddCartModule(this IServiceCollection services)
        {
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartRepository, CartRepository>();

            return services;
        }
    }
}
