using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Products;
using api.Dto;
using api.Interfaces;
using api.Mappers;

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

            await _productRepo.CrateAsync(entity);
            await _productRepo.SaveAsync();

            return ProductMapper.ToDto(entity);
        }

        public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductRequest updateProduct)
        {
            var existingProduct = await _productRepo.GetByIdAsync(id);
            if (existingProduct is null)
                throw new ArgumentException("Product does not exist");

            existingProduct.ApplyUpdate(updateProduct);
            await _productRepo.SaveAsync();
            return existingProduct.ToDto();         
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _productRepo.GetByIdAsync(id);

            if (product == null) return false;

            await _productRepo.DeleteAsync(id);
            return true;
        }

        public async Task<IReadOnlyList<ProductNameDto>> GetAllNamesAsync()
        {
            var names = await _productRepo.GetAllNamesAsync();
            return names;
        }
    }
}