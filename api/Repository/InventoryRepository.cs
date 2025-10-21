using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _dbContext;
        public InventoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task CreateAsync(Inventory entity)
        {
            return _dbContext.Inventory.AddAsync(entity).AsTask();
        }

        public Task<Inventory?> DeleteAsync(Guid id)
        {
            var inventoryModel = _dbContext.Inventory.FirstOrDefaultAsync(x => x.Id == id);
            if (inventoryModel == null) return null;
            _dbContext.Remove(inventoryModel);
            _dbContext.SaveChangesAsync();
            return inventoryModel;
        }

        public Task<Inventory?> GetByIdAsync(Guid id)
        {
            return _dbContext.Inventory.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Inventory?> GetByVariantAsync(Guid variantId)
        {
            var byVariant = _dbContext.Inventory.Where(i => i.VariantId == variantId);
            return byVariant.FirstOrDefaultAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}