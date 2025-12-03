using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Users;
using api.Extensions;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
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
        public async Task<ActionResult<AuthResult>> Login([FromBody] LoginUserDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authResult = await _authservice.LoginAsync(loginDto);

            if(authResult is null)
                return Unauthorized(new {message = "Invalid email or password"});

            return Ok(authResult);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await _authservice.RefeshAsync(request.RefreshToken);

            if(result is null)
                return Unauthorized(new {message = "Invalid or expired refresh token"});

            return Ok(result);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.GeUserId();
            
            await _authservice.LogoutAsync(userId);

            return NoContent();
        }
    }
}