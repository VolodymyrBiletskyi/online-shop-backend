using System.ComponentModel.DataAnnotations;

namespace api.Modules.UserModule.DTOs.Requests
{
    public class CreateUserDto
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; } = null!;
    }
}
