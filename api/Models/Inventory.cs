using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Inventory
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ProductId { get; set; }
        public Product Products { get; set; } = null!;
        public Guid? VariantId { get; set; }
        public ProductVariants? productVariants { get; set; }

        public int Quantity_on_hand { get; set; } = 0;
        public int Quantity_reserved { get; set; } = 0;

    }
}