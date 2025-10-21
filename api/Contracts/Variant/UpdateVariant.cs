using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace api.Contracts.Variant
{
    public class UpdateVariant
    {
        public string Sku { get; set; } = null!;
        public string? Title { get; set; }
        public decimal? PriceOverride { get; set; }
        public decimal? Weight { get; set; }
        public string? Attributes { get; set; }
    }
}