using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GeUserId(this ClaimsPrincipal user)
        {
            var id = user.FindFirst("userId")?.Value;

            if (string.IsNullOrEmpty(id))
                throw new InvalidOperationException("Claim not found in JWT");

            return Guid.Parse(id);
        }

        public static string? GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst("email")?.Value;
        } 
    }
}