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
            .ToListAsync();
        }

        public Task<Product?> GetByIdAsync(Guid id)
        {
            return _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task CrateAsync(Product entity)
        {
            return _dbContext.AddAsync(entity).AsTask();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Product?> DeleteAsync(Guid id)
        {
            var productModel = await GetByIdAsync(id);

            if (productModel == null) return null;

            _dbContext.Products.Remove(productModel);

            await _dbContext.SaveChangesAsync();
            return productModel;
        }

        public Task<Product?> GetWithVariantsAsync(Guid id)
        {
            return _dbContext.Products
                .Include(p => p.Variants)
                .Include(p => p.Images)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public Task<Product?> GetBySlugAsync(string slug)
        {
            return _dbContext.Products.SingleOrDefaultAsync(p => p.Slug == slug);
        }

        public Task<bool> SlugExistsAsync(string slug)
        {
            return _dbContext.Products.AnyAsync(p => p.Slug == slug);
        }
    }
}