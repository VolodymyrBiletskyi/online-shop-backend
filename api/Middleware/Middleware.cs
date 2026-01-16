using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.Extensions.Caching.Memory;

namespace api.Middleware
{
    public class Middleware : IMiddleware
    {
        private const string CacheKeyPrefix = "user-role:";
        private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(3);

        private readonly IUserRepository _userRepo;
        private readonly IMemoryCache _cache;
        public Middleware(IUserRepository userRepo, IMemoryCache cache)
        {
            _userRepo = userRepo;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.User?.Identity?.IsAuthenticated != true)
            {
                await next(context);
                return;
            }

            var userId = GetUserId(context.User);
            if (userId is null)
            {
                await next(context);
                return;
            }

            var cacheKey = CacheKeyPrefix + userId.Value.ToString("N");

            if (!_cache.TryGetValue(cacheKey, out string? roleName)|| string.IsNullOrWhiteSpace(roleName))
            {
                var role = await _userRepo.GetUserRoleAsync(userId.Value);

                if(role is null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                roleName = role.Value.ToString();
                _cache.Set(cacheKey,roleName, CacheTtl);
            }

            var identity = context.User.Identity as ClaimsIdentity;
            if(identity is not null && !context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
            }

            await next(context);
        }   

        private static Guid? GetUserId(ClaimsPrincipal user)
        {
            var raw = 
                user.FindFirstValue("userId") ??
                user.FindFirstValue(ClaimTypes.NameIdentifier) ??
                user.FindFirstValue("sub");

            return Guid.TryParse(raw, out var id) ? id : null;
        }    
    }
}