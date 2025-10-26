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
        public string Slug { get; set; } = null!;
        public string? Description { get; set; }
        public int SortOrder { get; set; }

        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}