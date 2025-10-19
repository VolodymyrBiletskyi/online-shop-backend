using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;

namespace api.Contracts.Users
{
    public class AuthResult
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpiresAtUtc { get; set; }
    }
}