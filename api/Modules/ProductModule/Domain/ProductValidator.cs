using api.Modules.ProductModule.DTOs.Requests;

namespace api.Modules.ProductModule.Domain
{
    public class ProductValidator : IProductValidator
    {
        public void ValidateCreateProduct(CreateProduct createProduct)
        {
            var validName = createProduct.Name.Trim();
            if (string.IsNullOrWhiteSpace(validName))
                throw new ArgumentException("Product name is required");

            if (createProduct.BasePrice < 0)
                throw new ArgumentException("Price can't be < 0");
        }
    }
}
