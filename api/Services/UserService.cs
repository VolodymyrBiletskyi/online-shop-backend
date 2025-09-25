using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.User;
using api.Interfaces;
using api.Mappers;
using api.Models;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _passwordHasher;
        public UserService(IUserRepository userRepo, IPasswordHasher passwordHasher)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken ct)
        {
            var existing = await _userRepo.GetByEmailAsync(dto.Email, ct);
            if (existing is not null)
                throw new InvalidOperationException("Email is already taken");

            var passwordHash = _passwordHasher.Hash(dto.Password);
            var entity = UserMapper.ToEntity(dto,passwordHash);
            
            await _userRepo.AddAsync(entity, ct);
            await _userRepo.SaveChangesAsync(ct);
            return UserMapper.ToDto(entity);
        }

        public async Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken ct)
        {
            var users = await _userRepo.GetAllAsync(ct);
            return users.Select(UserMapper.ToDto).ToList();
        }

        public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var user = await _userRepo.GetByIdAsync(id, ct);
            return user is null ? null : UserMapper.ToDto(user);
        }
    }
}