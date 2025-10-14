using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.User;
using api.Contracts.Users.Request;
using api.Contracts.Users.Response;
using api.Models;


namespace api.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken ct = default);

        Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken ct = default);
        Task<UserDto> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<UserDto> UpdateAsync(Guid id, UpdateUserDto updateDto, CancellationToken ct = default);
        Task<AuthResult> LoginAsync(LoginUserDto logindDto, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}