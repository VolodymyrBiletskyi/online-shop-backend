namespace api.Modules.UserModule.DTOs.Requests
{
    public class AddUserAddress
    {
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string NumOfObject { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public bool IsDefault { get; set; } = false;
    }
}
