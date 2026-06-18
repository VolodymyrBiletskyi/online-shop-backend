using api.Models;

namespace api.Modules.UserModule.DTOs.Responses
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
