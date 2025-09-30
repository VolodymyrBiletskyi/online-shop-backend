using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dto.User
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