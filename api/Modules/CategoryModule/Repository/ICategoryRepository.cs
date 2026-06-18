using api.Models;

namespace api.Modules.CategoryModule.Repository
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category entity);
        Task<int> SaveChangesAsync();
        Task<IReadOnlyList<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category?> DeleteAsync(Guid id);
    }
}
