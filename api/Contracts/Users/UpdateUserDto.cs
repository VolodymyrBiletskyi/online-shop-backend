using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Contracts.Users
{
    public class UpdateUserDto
    {
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}