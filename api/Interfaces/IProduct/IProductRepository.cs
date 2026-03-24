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
        Task CreateAsync(Product entity);
        Task<int> SaveAsync();
        Task<Product?> DeleteAsync(Guid id);
        Task<Product?> GetBySlugAsync(string slug);
        Task<bool> SlugExistsAsync(string slug);
        Task<decimal> GetPriceSnapshotAsync(Guid productId);
        Task<bool> IsSkuTakenAsync(string sku, Guid? excludeProductId = null);

    }
}