using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.User;
using api.Models;

namespace api.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken ct = default);
        
        Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken ct = default);
        Task<UserDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}