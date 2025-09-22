using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class CartItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CartId { get; set; }
        public Cart Cart { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public Guid? VariantId { get; set; }
        public ProductVariants? ProductVariant { get; set; }

        public int Quantity { get; set; }
        public decimal Unit_price_snapshot { get; set; }
    }
}