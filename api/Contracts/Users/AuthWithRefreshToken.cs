using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Contracts.Users
{
    public class AuthWithRefreshToken
    {
        public AuthResult Auth { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}