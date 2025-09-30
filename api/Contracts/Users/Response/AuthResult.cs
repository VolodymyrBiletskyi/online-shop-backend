using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.User;

namespace api.Contracts.Users.Response
{
    public class AuthResult
    {
        public UserDto User { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime ExpiresAtUtc { get; set; }
    }
}