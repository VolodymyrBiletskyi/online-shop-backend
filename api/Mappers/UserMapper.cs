using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Contracts.Users;
using api.Models;

namespace api.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public static User ToEntity(CreateUserDto dto, string passwordHash)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                FullName = dto.FullName,
                Role = UserRole.Customer,
                CreatedAt = DateTime.UtcNow,
                PasswordHash = passwordHash
            };
        }

        public static void ApplyUpdateFrom(this User entity, UpdateUserDto dto)
        {
            entity.Email = dto.Email;
            entity.FullName = dto.FullName;
            entity.PhoneNumber = dto.PhoneNumber;

        }

        public static AuthResult ToAuthResult(this User user, string token, DateTime expiresAt)
        {
            return new AuthResult
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Token = token,
                ExpiresAtUtc = expiresAt,
                
            };
        }

        public static UserAddress ToAddressEntity(AddUserAddress address)
        {
            return new UserAddress
            {
                Id = Guid.NewGuid(),
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                NumOfObject = address.NumOfObject,
                PostalCode = address.PostalCode,
                IsDefault = address.IsDefault
            };
        }

        public static UserAddressDto ToAddressDto(this UserAddress address)
        {
            return new UserAddressDto
            {
                Id = address.Id,
                UserId = address.UserId,
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                NumOfObject = address.NumOfObject,
                PostalCode = address.PostalCode,
                IsDefault = address.IsDefault
            };
        }
    }
}