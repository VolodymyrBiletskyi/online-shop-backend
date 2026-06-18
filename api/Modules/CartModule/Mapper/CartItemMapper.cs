using api.Models;
using api.Modules.CartModule.DTOs.Responses;

namespace api.Modules.CartModule.Mapper
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
