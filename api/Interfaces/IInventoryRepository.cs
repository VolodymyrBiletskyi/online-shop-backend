using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IInventoryRepository
    {
        Task CreateAsync(Inventory entity);
        Task<Inventory?> GetByIdAsync(Guid id);
        Task<Inventory?> GetByVariantAsync(Guid variantId);
        Task<Inventory?> DeleteAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}