using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Category;
using api.Dto;
using api.Interfaces;
using api.Mappers;

namespace api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public async Task<CategoryDto> CreateAsync(CreateCategory createCategory)
        {
            var entity = CategoryMapper.ToEntity(createCategory);

            await _categoryRepo.CreateAsync(entity);
            await _categoryRepo.SaveChangesAsync();
            return CategoryMapper.ToDto(entity);
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
        {
            var getCategories = await _categoryRepo.GetAllAsync();
            return getCategories.Select(CategoryMapper.ToDto).ToList();
        }

        public async Task<CategoryDto?> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            return category is null ? null : CategoryMapper.ToDto(category);
        }

        public async Task<CategoryDto> UpdateAsync(Guid id, UpdateCategory updateCategory)
        {
            var existingCategory = await _categoryRepo.GetByIdAsync(id);
            if (existingCategory is null)
                throw new ArgumentException("Category does not exist");

            existingCategory.ApplyUpdate(updateCategory);
            await _categoryRepo.SaveChangesAsync();
            return existingCategory.ToDto();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await _categoryRepo.DeleteAsync(id);
            if (category is null) return false;

            await _categoryRepo.SaveChangesAsync();
            return true;
        }
    }
}