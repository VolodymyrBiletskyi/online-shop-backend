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
        public Product Product { get; set; } = null!;
        public Guid VariantId { get; set; }
        public ProductVariant ProductVariant { get; set; } = null!;

        public int QuantityOnHand { get; set; } = 0;
        public int QuantityReserved { get; set; } = 0;

    }
}