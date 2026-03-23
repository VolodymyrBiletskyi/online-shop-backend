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
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;
        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _dbContext.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .ToListAsync();
        }

        public Task<Product?> GetByIdAsync(Guid id)
        {
            return _dbContext.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task CreateAsync(Product entity)
        {
            return _dbContext.Products.AddAsync(entity).AsTask();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Product?> DeleteAsync(Guid id)
        {
            var productModel = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (productModel == null) return null;

            _dbContext.Products.Remove(productModel);

            return productModel;
        }


        public Task<Product?> GetBySlugAsync(string slug)
        {
            return _dbContext.Products.SingleOrDefaultAsync(p => p.Slug == slug);
        }

        public Task<bool> SlugExistsAsync(string slug)
        {
            return _dbContext.Products.AnyAsync(p => p.Slug == slug);
        }

        public async Task<decimal> GetPriceSnapshotAsync(Guid productId)
        {
            var exists = await _dbContext.Products.AnyAsync(p => p.Id == productId);
            if (!exists)
                throw new InvalidOperationException("Product does not exist");

            return await _dbContext.Products
                .Where(p => p.Id == productId)
                .Select(p => p.BasePrice)
                .SingleAsync();
        }

        public Task<bool> IsSkuTakenAsync(string sku, Guid? excludeProductId = null)
        {
            var query = _dbContext.Products.Where(p => p.Sku == sku);

            if (excludeProductId.HasValue)
                query = query.Where(p => p.Id != excludeProductId.Value);

            return query.AnyAsync();
        }
    }
}