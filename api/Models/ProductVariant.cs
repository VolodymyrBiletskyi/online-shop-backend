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
        public Product Product { get; set; } = null!;

        public string Sku { get; set; } = string.Empty;
        public string? Title { get; set; }
        public decimal? PriceOverride { get; set; }
        public decimal? Weight { get; set; }
        public Dictionary<string, object> Attributes { get; set; } = new();
        public int InitAvaliable { get; set; }

        public ICollection<Inventory> InventoryItems { get; set; } = new List<Inventory>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();

    }
}