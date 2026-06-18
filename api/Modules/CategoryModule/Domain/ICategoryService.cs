using api.Modules.CategoryModule.DTOs.Requests;
using api.Modules.CategoryModule.DTOs.Responses;

namespace api.Modules.CategoryModule.Domain
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateAsync(CreateCategory createCategory);
        Task<IReadOnlyList<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(Guid id);
        Task<CategoryDto> UpdateAsync(Guid id, UpdateCategory updateCategory);
        Task<bool> DeleteAsync(Guid id);
    }
}
