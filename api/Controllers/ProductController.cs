using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Products;
using api.Contracts.Variant;
using api.Dto;
using api.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IVariantService _variantService;

        public ProductController(IProductService productService,IVariantService variantService)
        {
            _productService = productService;
            _variantService = variantService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost("create")]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProduct createProduct)
        {
            var created = await _productService.CreateAsync(createProduct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductDto>> Update(Guid id, [FromBody] UpdateProductRequest updateProduct)
        {
            var update = await _productService.UpdateAsync(id, updateProduct);
            return Ok(update);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted) return NotFound("Product dont't found");
            return Ok(new { message = "Product deleted successfully" });
        }

        
    }
}