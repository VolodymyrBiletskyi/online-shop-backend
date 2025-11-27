using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Models;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id); 
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User entity);
        Task<int> SaveChangesAsync();
        Task<User?> DeleteAsync(Guid id);
        Task AddAddressAsync(UserAddress address);
        Task<UserAddress?> GetDefaultUserAddressAsync(Guid userId);
        Task<bool> AddressExistsAsync(Guid userId, string street, string numOfObject);
        Task<IReadOnlyList<UserAddress>> GetAllUserAddresses(Guid userId);
        Task<UserAddress?> DeleteAddressAsync(Guid userId,Guid addressId);
        Task<UserAddress?> GetAddressById(Guid addressId);
    }
}