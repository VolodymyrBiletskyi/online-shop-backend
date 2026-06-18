using System.Security.Claims;

namespace api.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
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