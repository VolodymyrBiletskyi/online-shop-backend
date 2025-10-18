using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Products;
using api.Dto.Product;

namespace api.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductDto>> GetAllAsync();
        Task<ProductDto> CreateAsync(CreateProduct createProduct);
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<ProductDto> UpdateAsync(Guid id, UpdateProductRequest updateProduct);
        Task<bool> DeleteAsync(Guid id);
    }
}