namespace api.Modules.UserModule.DTOs.Requests
{
    public class UpdateAddress
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? NumOfObject { get; set; }
        public string? PostalCode { get; set; }
        public bool? IsDefault { get; set; } = false;
    }
}
