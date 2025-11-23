using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public Guid? VariantId { get; set; }
        public ProductVariant ProductVariant { get; set; } = null!;
        
        public string ProductNameSnapshot { get; set; } = null!;
        public string? VariantName { get; set; }
        public string SkuSnapshot { get; set; } = null!;
        public string? AttributesSnapshot { get; set; }

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalLine { get; set; }
    }
}