using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Product;
using api.Interfaces;
using api.Mappers;

namespace api.Services
{
    
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken ct = default)
        {
            var getProducts = await _productRepo.GetAllAsync(ct);
            return getProducts.Select(ProductMapper.ToDto).ToList();
        }
    }
}