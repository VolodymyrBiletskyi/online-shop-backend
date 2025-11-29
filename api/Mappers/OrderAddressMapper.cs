using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Models;

namespace api.Mappers
{
    public static class OrderAddressMapper
    {
        public static OrderAddressDto ToDto(this OrderAddress address)
        {
            return new OrderAddressDto
            {
                Id = address.Id,
                OrderId = address.OrderId,
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                BuildingNumber = address.BuildingNumber,
                PostalCode = address.PostalCode,
                Type = AddressType.Shipping
            };
        }
        
    }
}