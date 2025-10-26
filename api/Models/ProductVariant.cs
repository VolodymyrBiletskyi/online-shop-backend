using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace api.Models
{
    public class ProductVariant
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ProductId { get; set; }
        public Product Products { get; set; } = null!;

        public string Sku { get; set; } = null!;
        public string? Title { get; set; }
        public decimal? PriceOverride { get; set; }
        public decimal? Weight { get; set; }
        public JsonDocument Attributes { get; set; } = JsonDocument.Parse("{}");

        public ICollection<Inventory> InventoryItems { get; set; } = new List<Inventory>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

    }
}