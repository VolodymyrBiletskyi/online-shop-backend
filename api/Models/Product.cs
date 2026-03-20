using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int SortOrder { get; set; }
        public string Sku { get; set; } = null!;
        public Dictionary<string, string> Attributes { get; set; } = new();

        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; } = true;
        public int Available { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
        public ICollection<Inventory> InventoryItems { get; set; } = new List<Inventory>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
    }
}