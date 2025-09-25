using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class UserAddress
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid Userid { get; set; }
        public User User { get; set; } = null!;

        public UserAddresType Type { get; set; } = UserAddresType.Shipping;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string NumOfObject { get; set; } = null!;
        public string PostalCode { get; set; } = null!;

        public bool IsDefault { get; set; } = false;
    }
    
    public enum UserAddresType
    {
        Shipping = 0,
        Billing = 1
    }
}