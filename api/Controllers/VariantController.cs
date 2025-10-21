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
        public Task<ProductVariantDto> GetVariantById(Guid id)
        {
            var variant = _variantService.GetById(id);
            return variant;
        }

        [HttpPut("{id:guid}")]
        public Task<ProductVariantDto> UpdateVariant(Guid id, [FromBody] UpdateVariant updateVariant)
        {
            var update = _variantService.UpdateAsync(id, updateVariant);
            return update;
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var delete = await _variantService.DeleteAsync(id);

            if (!delete) return NotFound("Variant don't found");
            return Ok(new { message = "Variant deleted successfully" });
        }

        [HttpPost("{productId:guid}")]
        public Task<ProductVariantDto> CreateVariant (Guid productId,[FromBody] CreateVariant createVariant)
        {
            var create = _variantService.CreateForProductAsync(productId, createVariant);
            return create;
        }
    }
}