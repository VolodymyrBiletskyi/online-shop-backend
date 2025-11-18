using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Models;

namespace api.Mappers
{
    public static class CartItemMapper
    {
        public static CartItemDto ToDto(this CartItem item)
        {
            return new CartItemDto
            {
                Id = item.Id,
                ProductId = item.ProductId,
                VariantId = item.VariantId,
                ProductName = item.Product?.Name ?? string.Empty,
                VariantName = item.ProductVariant?.Title,
                UnitPrice = item.UnitPriceSnapshot,
                Quantity = item.Quantity
            };
        }
    }
}