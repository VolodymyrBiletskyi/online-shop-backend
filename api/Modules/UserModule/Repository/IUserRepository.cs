using api.Models;

namespace api.Modules.UserModule.Repository
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
        Task<bool> AddressExistsAsync(Guid userId, string street, string numOfObject, string city);
        Task<IReadOnlyList<UserAddress>> GetAllUserAddresses(Guid userId);
        Task<UserAddress?> DeleteAddressAsync(Guid userId, Guid addressId);
        Task<UserAddress?> GetAddressByIdAsync(Guid addressId);
        Task<UserRole?> GetUserRoleAsync(Guid userId);
        Task<IReadOnlyList<User>> GetAdminsAsync();
        Task<bool> AnyAdminExist(UserRole role);
    }
}
