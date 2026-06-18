using api.Models;

namespace api.Modules.UserModule.DTOs.Responses
{
    public class UserAddressDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public AddressType Type { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string NumOfObject { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public bool IsDefault { get; set; } = false;
    }
}
