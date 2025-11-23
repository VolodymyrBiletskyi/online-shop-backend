using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dto
{
    public class OrderItemDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? VariantId { get; set; }
        
        public string ProductNameSnapshot { get; set; } = null!;
        public string? VariantName { get; set; }
        public string SkuSnapshot { get; set; } = null!;
        public string? AttributesSnapshot { get; set; }

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalLine { get; set; }
    }
}