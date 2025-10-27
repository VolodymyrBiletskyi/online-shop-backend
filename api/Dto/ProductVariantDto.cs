using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DefaultValue("")]
        public string Sku { get; set; } = string.Empty;
        public string? Title { get; set; }
        public decimal? PriceOverride { get; set; }
        public decimal? Weight { get; set; }
        public Dictionary<string, object> Attributes { get; set; } = new();

        public decimal EffectivePrice { get; set; }
        public int Available { get; set; }
    }
}