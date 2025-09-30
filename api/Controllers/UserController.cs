using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.User;
using api.Contracts.Users.Request;
using api.Contracts.Users.Response;
using api.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet] //Get all Users
        public async Task<ActionResult<List<UserDto>>> GetAll(CancellationToken ct)
        {
            var users = await _userService.GetAllAsync(ct);
            return Ok(users);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id, CancellationToken ct)
        {
            var user = await _userService.GetByIdAsync(id, ct);
            return user is null ? NotFound() : Ok(user);
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto, CancellationToken ct)
        {
            var created = await _userService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("id:guid")]
        public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UpdateUserDto updateDto, CancellationToken ct)
        {
            try
            {
                var update = await _userService.UpdateAsync(id, updateDto, ct);
                return Ok(update);

            }
            catch (InvalidOperationException ex) when (ex.Message == "User does not exist")
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex) when (ex.Message == "Email is already taken")
            {
                return Conflict(new { message = ex.Message });
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResult>> Login([FromBody]LoginUserDto loginDto,CancellationToken ct)
        {
            var authResult = await _userService.LoginAsync(loginDto, ct);
        

            Response.Cookies.Append("AuthToken", authResult.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = new DateTimeOffset(authResult.ExpiresAtUtc)
            });
            return Ok(authResult);
        }

        
    }
}