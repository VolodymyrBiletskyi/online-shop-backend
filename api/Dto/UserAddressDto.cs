using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dto
{
    public class UserAddressDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserAddresType Type { get; set; } 
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string NumOfObject { get; set; } = null!;
        public string PostalCode { get; set; } = null!;

        public bool IsDefault { get; set; } = false;
    }
}