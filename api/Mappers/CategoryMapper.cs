using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Category;
using api.Dto;
using api.Models;

namespace api.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                SortOrder = category.SortOrder,
                ParentId = category.ParentId
            };
        }

        public static void ApplyUpdate(this Category entity,UpdateCategory updateCategory)
        {
            entity.Name = updateCategory.Name;
            entity.Slug = string.IsNullOrWhiteSpace(updateCategory.Slug)
                ? GenerateSlug(updateCategory.Name)
                : updateCategory.Slug;
            entity.SortOrder = updateCategory.SortOrder;
            entity.ParentId = updateCategory.ParentId;

        }

        public static Category ToEntity(this CreateCategory createCategory)
        {
            return new Category
            {
                Id = Guid.NewGuid(),
                Name = createCategory.Name,
                Slug =string.IsNullOrWhiteSpace(createCategory.Slug)
                    ?GenerateSlug(createCategory.Name)
                    :createCategory.Slug,
                SortOrder = createCategory.SortOrder,
                ParentId = createCategory.ParentId
            };
        }
        
        private static string GenerateSlug(string name)
            => name.Trim().ToLowerInvariant()
                    .Replace(' ', '-')
                    .Replace("--", "-");
    }
}