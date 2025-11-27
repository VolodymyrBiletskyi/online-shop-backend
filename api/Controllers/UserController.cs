using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Contracts.Users;
using api.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens.Experimental;

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

        [HttpGet] 
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto)
        {
            var created = await _userService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("id:guid")]
        public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UpdateUserDto updateDto)
        {
            try
            {
                var update = await _userService.UpdateAsync(id, updateDto);
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
        public async Task<ActionResult<AuthResult>> Login([FromBody] LoginUserDto loginDto)
        {
            var authResult = await _userService.LoginAsync(loginDto);

            return Ok(authResult);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _userService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPost("{userId}/address")]
        public async Task<ActionResult<UserAddressDto>> AddUserAddress([FromRoute] Guid userId,[FromBody] AddUserAddress address)
        {
            var userAddress = await _userService.AddAddressAsync(userId, address);
            return Ok(userAddress);
        }

        [HttpGet("{userId}/address/default")]
        public async Task<ActionResult<UserAddressDto>> GetUserAdress([FromRoute] Guid userId)
        {
            var userAddress = await _userService.GetDefaultUserAddressAsync(userId);
            return Ok(userAddress);
        }

        [HttpGet("{userId}/addresses")]
        public async Task<ActionResult<List<UserAddressDto>>> GetAllUserAddresses([FromRoute] Guid userId)
        {
            var addresses = await _userService.GetAllUserAddressesAsync(userId);
            return Ok(addresses);
        }
        
        [HttpDelete("{userId}/addresses/{addressId}")]
        public async Task<ActionResult<UserAddressDto>> DeleteUserAddress([FromRoute]Guid userId, [FromRoute] Guid addressId)
        {
            var userAddress = await _userService.DeleteUserAddressAsync(userId,addressId);
            return Ok(userAddress);
        }
    }
}