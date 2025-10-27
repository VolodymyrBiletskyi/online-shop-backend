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
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly AppDbContext _dbcontext;

        public ProductVariantRepository(AppDbContext dbContext)
        {
            _dbcontext = dbContext;            
        }

        public Task CreateAsync(ProductVariant entity)
        {
            return _dbcontext.AddAsync(entity).AsTask();
        }

        public async Task<ProductVariant?> DeleteAsync(Guid id)
        {
            var productVariantModel = await _dbcontext.ProductVariants.FirstOrDefaultAsync(x => x.Id == id);

            if (productVariantModel == null) return null;

            _dbcontext.ProductVariants.Remove(productVariantModel);
            await _dbcontext.SaveChangesAsync();
            
            return productVariantModel;
        }

        public Task<ProductVariant?> FindByAttributesAsync(Guid productId, string attributesJson)
        {
            return _dbcontext.ProductVariants
                .Where(v => v.ProductId == productId && EF.Functions.JsonContains(v.Attributes, attributesJson))
                .SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<ProductVariant>> GetAllAsync()
        {
            return await _dbcontext.ProductVariants
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<ProductVariant?> GetByIdAsync(Guid id)
        {
            return _dbcontext.ProductVariants.Include(v => v.Product)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public Task<IReadOnlyList<ProductVariant>> GetByProductAsync(Guid productId)
        {
            return _dbcontext.ProductVariants.Where(v => v.ProductId == productId).ToListAsync()
                .ContinueWith(t => (IReadOnlyList<ProductVariant>) t.Result);
        }

        public Task<ProductVariant?> GetBySkuAsync(string sku)
        {
            return _dbcontext.ProductVariants.SingleOrDefaultAsync(s => s.Sku == sku);
        }

        public Task<ProductVariant?> GetWithPRoductsAndInventoryAsync(Guid id)
        {
            return _dbcontext.ProductVariants
                .Include(v => v.Product)
                .Include(v => v.InventoryItems)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbcontext.SaveChangesAsync();
        }

        public Task<bool> SkuExistsAsync(string sku)
        {
            return _dbcontext.ProductVariants.AnyAsync(s => s.Sku == sku);
        }
    }
}