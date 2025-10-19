using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Contracts.Users;
using api.Models;


namespace api.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserDto>> GetAllAsync();

        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task<UserDto> GetByIdAsync(Guid id);
        Task<UserDto> UpdateAsync(Guid id, UpdateUserDto updateDto);
        Task<AuthResult> LoginAsync(LoginUserDto logindDto);
        Task<bool> DeleteAsync(Guid id);
    }
}