using api.Modules.ProductModule.DTOs.Requests;
using api.Modules.ProductModule.DTOs.Responses;

namespace api.Modules.ProductModule.Domain
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task<ProductDto> CreateAsync(CreateProduct createProduct);
        Task<ProductDto> UpdateAsync(Guid id, UpdateProductRequest updateProduct);
        Task<bool> DeleteAsync(Guid id);
    }
}
