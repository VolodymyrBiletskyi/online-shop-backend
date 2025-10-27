using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Variant;
using api.Dto;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/variants")]
    public class VariantController : ControllerBase
    {
        private readonly IVariantService _variantService;
        public VariantController(IVariantService variantService)
        {
            _variantService = variantService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductVariantDto>>> GetAll()
        {
            var variants = await _variantService.GetAllAsync();
            return Ok(variants);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductVariantDto>> GetVariantById(Guid id)
        {
            var variant = await _variantService.GetById(id);
            return variant is null ? NotFound() : Ok(variant);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductVariantDto>> UpdateVariant(Guid id, [FromBody] UpdateVariant updateVariant)
        {
            var update = await _variantService.UpdateAsync(id, updateVariant);
            return update is null ? NotFound() : Ok(update);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _variantService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPost("~/api/products/{productId:guid}/variants")]
        public async Task<ActionResult<ProductVariantDto>> CreateVariant(Guid productId, [FromBody] CreateVariant createVariant)
        {
            var create = await _variantService.CreateForProductAsync(productId, createVariant);
            return CreatedAtAction(nameof(GetVariantById), new { id = create.Id }, create);
        }

        [HttpGet("~/api/products/{productId:guid}/variants")]
        public async Task<ActionResult<IReadOnlyList<ProductVariantDto>>> GetVariantsByProductId(Guid productId)
        {
            var productVariants = await _variantService.GetVariantsByProductIdAsync(productId);
            return productVariants is null ? NotFound() : Ok(productVariants);
        }
    }
}