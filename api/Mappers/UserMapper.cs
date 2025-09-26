using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.User;
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
    }
}