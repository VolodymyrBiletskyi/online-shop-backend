namespace api.Modules.UserModule.DTOs.Requests
{
    public class UpdateUserDto
    {
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}
