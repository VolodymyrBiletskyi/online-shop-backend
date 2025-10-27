using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Variant;
using api.Interfaces;
using api.Mappers;
using api.Models;
using System.Text.Json;
using api.Dto;
using Humanizer;
using api.Extensions;

namespace api.Services
{
    public class VariantService : IVariantService
    {
        private readonly IProductRepository _productRepo;
        private readonly IProductVariantRepository _variantRepo;
        private readonly IInventoryRepository _inventory;

        public VariantService(IProductRepository productRepo, IProductVariantRepository variantRepo,
        IInventoryRepository inventory)
        {
            _productRepo = productRepo;
            _variantRepo = variantRepo;
            _inventory = inventory;
        }

        public async Task<ProductVariantDto> CreateForProductAsync(Guid productId, CreateVariant createVariant)
        {
            var product = await _productRepo.GetByIdAsync(productId)
                ?? throw new ArgumentException("Product don't found");

            var variant = VariantMapper.ToEntity(createVariant, productId);
 
            if (await _variantRepo.SkuExistsAsync(createVariant.Sku))
                variant.Sku = SkuGenerator.Generate(createVariant.Title);            

            await _variantRepo.CreateAsync(variant);
            await _variantRepo.SaveChangesAsync();

            var inventory = new Inventory
            {
                ProductId = product.Id,
                VariantId = variant.Id,
                QuantityOnHand = createVariant.InitAvailable,
                QuantityReserved = 0
            };

            await _inventory.CreateAsync(inventory);
            await _inventory.SaveChangesAsync();

            var loaded = await _variantRepo.GetWithPRoductsAndInventoryAsync(variant.Id)
                ?? throw new ArgumentException();

            return loaded.ToDtoWithAggregates();
        }

        public async Task<ProductVariantDto> GetById(Guid id)
        {
            var variant = await _variantRepo.GetByIdAsync(id)
                ?? throw new ArgumentException("Variant not found");

            var product = await _productRepo.GetByIdAsync(variant.ProductId)
                ?? throw new ArgumentException("Product not found");

            var inventory = await _inventory.GetByVariantAsync(id)
                ?? new Inventory { QuantityOnHand = 0, QuantityReserved = 0 };

            var dto = variant.ToDto();

            dto.EffectivePrice = variant.PriceOverride ?? product.BasePrice;
            dto.Available = inventory.QuantityOnHand - inventory.QuantityReserved;
            return dto;
        }

        public async Task<ProductVariantDto> UpdateAsync(Guid id, UpdateVariant updateVariant)
        {
            var variant = await _variantRepo.GetByIdAsync(id)
                ?? throw new ArgumentException("Variant not found");

            if (!string.IsNullOrWhiteSpace(updateVariant.Sku) &&
            !updateVariant.Sku.Equals(variant.Sku, StringComparison.OrdinalIgnoreCase))
            {
                if (await _variantRepo.SkuExistsAsync(updateVariant.Sku))
                    throw new InvalidOperationException("Sku already exists");
                variant.Sku = updateVariant.Sku.Trim().ToUpperInvariant();
            }

            VariantMapper.ApplyUpdate(variant, updateVariant);
            await _variantRepo.SaveChangesAsync();

            var loaded = await _variantRepo.GetWithPRoductsAndInventoryAsync(variant.Id)
                ?? throw new ArgumentException();

            return loaded.ToDtoWithAggregates(); 
            
            
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var variant = await _variantRepo.GetByIdAsync(id);
            if (variant == null) return false;
            await _variantRepo.DeleteAsync(id);
            return true;
            
        }

        public async Task<IReadOnlyList<ProductVariantDto>> GetAllAsync()
        {
            var variants = await _variantRepo.GetAllAsync();
            return variants.Select(VariantMapper.ToDto).ToList();
        }

        public async Task<IReadOnlyList<ProductVariantDto>> GetVariantsByProductIdAsync(Guid productId)
        {
            var productVariants = await _variantRepo.GetByProductAsync(productId);
            return productVariants.Select(VariantMapper.ToDto).ToList();
        }
    }
}