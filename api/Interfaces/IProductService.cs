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
    }
}