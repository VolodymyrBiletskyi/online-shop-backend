using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto
{
    public class InventoryDto
    {
        public Guid Id { get; set; }

        public Guid? VariantId { get; set; }
        public int QuantityOnHand { get; set; } 
        public int QuantityReserved { get; set; }
        public int Available => QuantityOnHand - QuantityReserved;
    }
}