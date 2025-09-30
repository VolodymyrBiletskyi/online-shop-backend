using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.User;
using api.Contracts.Users.Request;
using api.Contracts.Users.Response;
using api.Interfaces;
using api.Mappers;
using Humanizer;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        public UserService(IUserRepository userRepo, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
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

        public async Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken ct)
        {
            var existingemail = await _userRepo.GetByEmailAsync(dto.Email, ct);
            if (existingemail is not null)
            {
                throw new InvalidOperationException("Email is already taken");
            }

            var passwordHash = _passwordHasher.Hash(dto.Password);
            var entity = UserMapper.ToEntity(dto,passwordHash);
            
            await _userRepo.AddAsync(entity, ct);
            await _userRepo.SaveChangesAsync(ct);
            return UserMapper.ToDto(entity);
        }

        public async Task<UserDto> UpdateAsync(Guid id, UpdateUserDto updateDto, CancellationToken ct = default)
        {
            var existingUser = await _userRepo.GetByIdAsync(id, ct);
            if (existingUser is null)
                throw new InvalidOperationException("User does not exist");

            var newEmail = updateDto.Email.Trim();
            var emailChanged = !string.Equals(existingUser.Email, newEmail, StringComparison.OrdinalIgnoreCase);

            if (emailChanged)
            {
                var byEmail = await _userRepo.GetByEmailAsync(newEmail, ct);

                if (byEmail is not null && byEmail.Id != id)
                    throw new InvalidOperationException("Email is already taken");
                existingUser.Email = newEmail!;
            }

            existingUser.ApplyUpdateFrom(updateDto);

            await _userRepo.SaveChangesAsync(ct);
            return existingUser.ToDto();
            
        }

        public async Task<AuthResult> LoginAsync(LoginUserDto logindDto, CancellationToken ct = default)
        {
            var user = await _userRepo.GetByEmailAsync(logindDto.Email)
                ?? throw new Exception("User not found");

            var result = _passwordHasher.Verify(logindDto.Password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateToken(user);

            var parsed = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var expiresAt = parsed.ValidTo;

            return user.ToAuthResult(token, expiresAt);

        }
    }
}