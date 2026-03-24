using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Models;

namespace api.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category entity);
        Task<int> SaveChangesAsync();
        Task<IReadOnlyList<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(Guid id);
        Task<Category> DeleteAsync(Guid id); 
    }
}