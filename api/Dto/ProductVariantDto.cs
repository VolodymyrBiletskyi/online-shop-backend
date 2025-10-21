using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Models;

namespace api.Dto
{
    public class ProductVariantDto
    {
        public Guid Id { get; set; } 

        public Guid ProductId { get; set; }
        public string Sku { get; set; } = null!;
        public string? Title { get; set; }
        public decimal? PriceOverride { get; set; }
        public decimal? Weight { get; set; }
        public JsonElement? Attributes { get; set; }

        public decimal EffectivePrice { get; set; }
        public int Available { get; set; }
    }
}