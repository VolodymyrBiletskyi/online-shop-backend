using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Products;
using api.Interfaces;

namespace api.Validators
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