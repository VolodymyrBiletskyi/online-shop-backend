using api.Modules.CategoryModule.Domain;
using api.Modules.CategoryModule.Repository;

namespace api.Modules.CategoryModule
{
    public static class CategoryModuleExtensions
    {
        public static IServiceCollection AddCategoryModule(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
