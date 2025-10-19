using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Category;
using api.Dto;
using api.Models;

namespace api.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateAsync(CreateCategory createCategory);
        Task<IReadOnlyList<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(Guid id);
        Task<CategoryDto> UpdateAsync(Guid id,UpdateCategory updateCategory);
        Task<bool> DeleteAsync(Guid id);
    }
}