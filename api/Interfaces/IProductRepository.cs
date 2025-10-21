using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task CrateAsync(Product entity);
        Task<int> SaveAsync();
        Task<Product?> DeleteAsync(Guid id);
        Task<Product?> GetWithVariantsAsync(Guid id);
        Task<Product?> GetBySlugAsync(string slug);
        Task<bool> SlugExistsAsync(string slug);
    }
}