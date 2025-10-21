using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Models;

namespace api.Mappers
{
    public static class InventoryMapper
    {
        public static InventoryDto ToDto(this Inventory entity)
        {
            return new InventoryDto
            {
                VariantId = entity.VariantId,
                QuantityOnHand = entity.QuantityOnHand,
                QuantityReserved = entity.QuantityReserved
            };
        }

        public static Inventory ToEntity(InventoryDto dto)
        {
            return new Inventory
            {
                VariantId = dto.VariantId,
                QuantityOnHand = dto.QuantityOnHand,
                QuantityReserved = dto.QuantityReserved
            };
        }



    }
}