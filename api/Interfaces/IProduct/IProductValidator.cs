using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Products;

namespace api.Interfaces
{
    public interface IProductValidator
    {
        void ValidateCreateProduct(CreateProduct createProduct);
    }
}