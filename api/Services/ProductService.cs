using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Products;
using api.Dto;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;

namespace api.Services
{

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IProductValidator _validator;
        public ProductService(IProductRepository productRepo, IProductValidator validator)
        {
            _productRepo = productRepo;
            _validator = validator;
        }
        private async Task EnsureSkuAsync(Product product, string productName)
        {
            var shouldGenerate =
                string.IsNullOrWhiteSpace(product.Sku) ||
                await _productRepo.IsSkuTakenAsync(product.Sku, product.Id);

            if (!shouldGenerate)
                return;

            product.Sku = SkuGenerator.Generate(productName);
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllAsync()
        {
            var getProducts = await _productRepo.GetAllAsync();
            return getProducts.Select(ProductMapper.ToDto).ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            return product is null ? null : ProductMapper.ToDto(product);
        }

        public async Task<ProductDto> CreateAsync(CreateProduct createProduct)
        {
            _validator.ValidateCreateProduct(createProduct);

            var entity = ProductMapper.ToEntity(createProduct);

            await EnsureSkuAsync(entity, createProduct.Name);

            await _productRepo.CreateAsync(entity);
            await _productRepo.SaveAsync();

            return entity.ToDto();
        }

        public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductRequest updateProduct)
        {
            var existingProduct = await _productRepo.GetByIdAsync(id)
                ?? throw new ArgumentException("Product does not exist");

            existingProduct.ApplyUpdate(updateProduct);

            await EnsureSkuAsync(existingProduct, existingProduct.Name);

            await _productRepo.SaveAsync();

            return existingProduct.ToDto();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _productRepo.GetByIdAsync(id);

            if (product == null) return false;

            await _productRepo.DeleteAsync(id);
            await _productRepo.SaveAsync();
            return true;
        }
    }
}