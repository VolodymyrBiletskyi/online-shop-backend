using api.Extensions;
using api.Modules.AuthModule.Domain;
using api.Modules.AuthModule.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Modules.AuthModule.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authservice;

        public AuthController(IAuthService authService)
        {
            _authservice = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResult>> Login([FromBody] DTOs.Requests.LoginUserDto loginDto)
        {
            var authResult = await _authservice.LoginAsync(loginDto);
            if (authResult is null)
                return Unauthorized(new { message = "Invalid email or password" });

            Response.Cookies.Append("refreshToken", authResult.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(authResult.Auth);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var rawRefreshToken))
                return Unauthorized(new { message = "No refresh token" });

            var result = await _authservice.RefreshAsync(rawRefreshToken);
            if (result is null)
                return Unauthorized(new { message = "Invalid or expired refresh token" });

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(result.Auth);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.GetUserId();

            await _authservice.LogoutAsync(userId);

            Response.Cookies.Delete("refreshToken");

            return NoContent();
        }
    }
}
