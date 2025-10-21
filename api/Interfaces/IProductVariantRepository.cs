using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IProductVariantRepository
    {
        Task CreateAsync(ProductVariant entity);
        Task<IReadOnlyList<ProductVariant>> GetAllAsync();
        Task<ProductVariant?> GetByIdAsync(Guid id);
        Task<ProductVariant?> GetBySkuAsync(string sku);
        Task<bool> SkuExistsAsync(string sku);
        Task<ProductVariant?> GetWithPRoductsAndInventoryAsync(Guid id);
        Task<IReadOnlyList<ProductVariant>> GetByProductAsync(Guid productId);
        Task<ProductVariant?> FindByAttributesAsync(Guid productId, string attributesJson);
        Task<int> SaveChangesAsync();
        Task<ProductVariant?> DeleteAsync(Guid id);
    }
}