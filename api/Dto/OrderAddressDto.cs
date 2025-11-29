using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dto
{
    public class OrderAddressDto
    {
        public Guid Id { get; set; } 

        public Guid OrderId { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string BuildingNumber { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public AddressType Type { get; set; }

    }
}