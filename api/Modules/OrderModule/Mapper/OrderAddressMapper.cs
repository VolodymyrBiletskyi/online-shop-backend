using api.Models;
using api.Modules.OrderModule.DTOs.Responses;

namespace api.Modules.OrderModule.Mapper
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
