using api.Modules.OrderModule.Domain;
using api.Modules.OrderModule.Repository;

namespace api.Modules.OrderModule
{
    public static class OrderModuleExtensions
    {
        public static IServiceCollection AddOrderModule(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
