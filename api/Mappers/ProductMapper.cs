using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Products;
using api.Dto;
using api.Models;

namespace api.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Name = product.Name,
                CategoryName = product.Category?.Name,
                Slug = product.Slug,
                Description = product.Description,
                SortOrder = product.SortOrder,
                BasePrice = product.BasePrice,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt

            };
        }

        public static void ApplyUpdate(this Product entity,UpdateProductRequest updateProduct)
        {
            entity.CategoryId = updateProduct.CategoryId;
            entity.Name = updateProduct.Name;
            entity.Slug = string.IsNullOrWhiteSpace(updateProduct.Slug)
                ? GenerateSlug(updateProduct.Name)
                : updateProduct.Slug;
            entity.Description = updateProduct.Description;
            entity.SortOrder = updateProduct.SortOrder;
            entity.BasePrice = updateProduct.BasePrice;
            entity.IsActive = updateProduct.IsActive;
        }

        public static Product ToEntity(this CreateProduct createProduct)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = createProduct.CategoryId,
                Name = createProduct.Name,
                Slug = string.IsNullOrWhiteSpace(createProduct.Slug)
                    ?GenerateSlug(createProduct.Name)
                    :createProduct.Slug,
                Description = createProduct.Description,
                SortOrder = createProduct.SortOrder,
                BasePrice = createProduct.BasePrice,
                IsActive = createProduct.IsActive,
                CreatedAt = DateTime.UtcNow
            };
        }


        private static string GenerateSlug(string name)
            => name.Trim().ToLowerInvariant()
                    .Replace(' ', '-')
                    .Replace("--", "-");
    }
}