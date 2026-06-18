using api.Modules.UserModule.Domain;
using api.Modules.UserModule.Repository;

namespace api.Modules.UserModule
{
    public static class UserModuleExtensions
    {
        public static IServiceCollection AddUserModule(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserValidator, UserValidator>();

            return services;
        }
    }
}
