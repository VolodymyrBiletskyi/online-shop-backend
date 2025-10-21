using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Contracts.Variant;
using api.Dto;
using api.Extensions;
using api.Models;
using api.Validators;

namespace api.Mappers
{
    public static class VariantMapper
    {
        public static ProductVariant ToEntity(this CreateVariant createVariant,Guid productId)
        {
            return new ProductVariant
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Sku = createVariant.Sku.Trim().ToUpperInvariant(),
                Title = createVariant.Title,
                PriceOverride = createVariant.PriceOverride,
                Weight = createVariant.Weight,
                Attributes = JsonDocHelper.ParseOrEmpty(createVariant.Attributes)
            };
        }

        public static void ApplyUpdate(this ProductVariant entity, UpdateVariant updateVariant)
        {
            entity.Sku = updateVariant.Sku.Trim().ToUpperInvariant();
            entity.Title = updateVariant.Title;
            entity.PriceOverride = updateVariant.PriceOverride;
            entity.Weight = updateVariant.Weight;
            entity.Attributes = JsonDocHelper.ParseOrEmpty(updateVariant.Attributes);
                
        }
        public static ProductVariantDto ToDto(this ProductVariant productVariant)
        {
            return new ProductVariantDto
            {
                Id = productVariant.Id,
                ProductId = productVariant.ProductId,
                Title = productVariant.Title,
                PriceOverride = productVariant.PriceOverride,
                Weight = productVariant.Weight,
                Attributes = productVariant.Attributes.RootElement.Clone()
            };
        }

        public static ProductVariantDto ToDtoWithAggregates(this ProductVariant v)
        {
            var dto = v.ToDto();
            var basePrice = v.Products.BasePrice; 
            var available = v.InventoryItems.Sum(i => i.QuantityOnHand - i.QuantityReserved);
            dto.EffectivePrice = v.PriceOverride ?? basePrice;
            dto.Available = available;
            return dto;
        }
    }
}