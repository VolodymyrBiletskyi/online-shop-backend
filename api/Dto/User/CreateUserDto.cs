using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dto.User
{
    public class CreateUserDto
    {

        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } = null!;
    }
}