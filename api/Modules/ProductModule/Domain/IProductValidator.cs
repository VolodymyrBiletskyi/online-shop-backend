using api.Modules.ProductModule.DTOs.Requests;

namespace api.Modules.ProductModule.Domain
{
    public interface IProductValidator
    {
        void ValidateCreateProduct(CreateProduct createProduct);
    }
}
