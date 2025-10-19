using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dto;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;
        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task CreateAsync(Category entity)
        {
            return _dbContext.AddAsync(entity).AsTask();
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            return await _dbContext.Categories
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var categoryModel = await GetByIdAsync(id);
            if (categoryModel == null) return null;

            _dbContext.Categories.Remove(categoryModel);
            
            await SaveChangesAsync();
            return categoryModel;
        }
    }
}