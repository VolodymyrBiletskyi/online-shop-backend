using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace api.Contracts.Variant
{
    public class CreateVariant
    {   
        public string? Title { get; set; }
        [DefaultValue("")]
        public string Sku { get; set; } = string.Empty;
        public decimal? PriceOverride { get; set; }
        public decimal? Weight { get; set; }

        public Dictionary<string, object> Attributes { get; set; } = new();

        public int InitAvailable { get; set; } = 0;
    }
}