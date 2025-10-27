using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Variant;
using api.Dto;
using api.Models;

namespace api.Interfaces
{
    public interface IVariantService
    {
        Task<IReadOnlyList<ProductVariantDto>> GetAllAsync();
        Task<ProductVariantDto> CreateForProductAsync(Guid productId, CreateVariant createVariant);
        Task<ProductVariantDto> UpdateAsync(Guid id, UpdateVariant updateVariant);
        Task<bool> DeleteAsync(Guid id);
        Task<ProductVariantDto> GetById(Guid id);
        Task<IReadOnlyList<ProductVariantDto>> GetVariantsByProductIdAsync(Guid productId);
    }
}