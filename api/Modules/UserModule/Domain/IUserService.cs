using api.Modules.UserModule.DTOs.Requests;
using api.Modules.UserModule.DTOs.Responses;

namespace api.Modules.UserModule.Domain
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(Guid id);
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task<UserDto> UpdateAsync(Guid id, UpdateUserDto updateDto);
        Task<bool> DeleteAsync(Guid id);
        Task<UserAddressDto> AddAddressAsync(Guid userId, AddUserAddress address);
        Task<UserAddressDto?> GetDefaultUserAddressAsync(Guid userId);
        Task<IReadOnlyList<UserAddressDto>> GetAllUserAddressesAsync(Guid userId);
        Task<UserAddressDto?> DeleteUserAddressAsync(Guid userId, Guid addressId);
        Task<UserAddressDto> UpdateAddressAsync(Guid addressId, UpdateAddress update);
        Task<UserAddressDto> UpdateAddressDefaultAsync(Guid addressId);
        Task<IReadOnlyList<UserDto>> GetAllAdminsAsync();
        Task EnsureAdminExistsAsync(string email, string password);
    }
}
