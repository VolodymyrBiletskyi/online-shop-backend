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
                ProductName = item.Product?.Name ?? string.Empty,
                Sku = item.SkuSnapshot,
                UnitPrice = item.UnitPriceSnapshot,
                Quantity = item.Quantity
            };
        }
    }
}