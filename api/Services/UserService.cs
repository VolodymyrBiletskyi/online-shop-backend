using System.IdentityModel.Tokens.Jwt;
using api.Dto;
using api.Contracts.Users;
using api.Interfaces;
using api.Mappers;


namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserValidator _validator;
        public UserService(IUserRepository userRepo, IPasswordHasher passwordHasher, IUserValidator validator)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
            _validator = validator;

        }

        public async Task<IReadOnlyList<UserDto>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Select(UserMapper.ToDto).ToList();
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            return user is null ? null : UserMapper.ToDto(user);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            _validator.ValidateCreateUser(dto);

            var email = dto.Email.Trim().ToLowerInvariant(); 
            if (await _userRepo.GetByEmailAsync(email) is not null)
                throw new InvalidOperationException("Email is already taken");
        
            var passwordHash = _passwordHasher.Hash(dto.Password);

            var entity = UserMapper.ToEntity(dto,passwordHash);
            
            await _userRepo.AddAsync(entity);
            await _userRepo.SaveChangesAsync();
            return UserMapper.ToDto(entity);
        }

        public async Task<UserDto> UpdateAsync(Guid id, UpdateUserDto updateDto)
        {
            var existingUser = await _userRepo.GetByIdAsync(id);
            if (existingUser is null)
                throw new InvalidOperationException("User does not exist");

            var newEmail = updateDto.Email.Trim();
            var emailChanged = !string.Equals(existingUser.Email, newEmail, StringComparison.OrdinalIgnoreCase);

            if (emailChanged)
            {
                var byEmail = await _userRepo.GetByEmailAsync(newEmail);

                if (byEmail is not null && byEmail.Id != id)
                    throw new InvalidOperationException("Email is already taken");
                existingUser.Email = newEmail!;
            }

            existingUser.ApplyUpdateFrom(updateDto);

            await _userRepo.SaveChangesAsync();
            return existingUser.ToDto();
            
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepo.DeleteAsync(id);
            return true;
        }

        public async Task<UserAddressDto> AddAddressAsync(Guid userId,AddUserAddress address)
        {
            var user = await _userRepo.GetByIdAsync(userId)
                ?? throw new InvalidOperationException("User does not exist");

            var street = address.Street;
            var numOfObject = address.NumOfObject;
            var city = address.City;

            var exists = await _userRepo.AddressExistsAsync(userId,street,numOfObject,city);
            if(exists)
                throw new InvalidOperationException("This address already added");

            var userAddress = UserMapper.ToAddressEntity(address);
            userAddress.UserId = userId;

            var existingDefault = await _userRepo.GetDefaultUserAddressAsync(userId);
            if(address.IsDefault == true && existingDefault !=null) 
                throw new InvalidOperationException("User can not have more then 1 default address");

            await  _userRepo.AddAddressAsync(userAddress);
            await _userRepo.SaveChangesAsync();

            return userAddress.ToAddressDto();
        }

        public async Task<UserAddressDto?> GetDefaultUserAddressAsync(Guid userId)
        {
            var user = await _userRepo.GetByIdAsync(userId)
                ?? throw new InvalidOperationException("User does not exist");

            var address = await _userRepo.GetDefaultUserAddressAsync(userId);
            return address?.ToAddressDto();
        }

        public async Task<IReadOnlyList<UserAddressDto>> GetAllUserAddressesAsync(Guid userId)
        {
            var user = await _userRepo.GetByIdAsync(userId)
                ?? throw new InvalidOperationException("User does not exist");

            var addresses = await _userRepo.GetAllUserAddresses(userId);

            return addresses.Select(UserMapper.ToAddressDto).ToList();
            
        }

        public async Task<UserAddressDto?> DeleteUserAddressAsync(Guid userId, Guid addressId)
        {
            var user = await _userRepo.GetByIdAsync(userId)
                ?? throw new InvalidOperationException("User does not exist");

            var existingAddress = await _userRepo.GetAddressByIdAsync(addressId)
                ?? throw new InvalidOperationException("Address does not exist");

            var address = await _userRepo.DeleteAddressAsync(userId,addressId)
                ?? throw new InvalidOperationException("Failed to delete address");
            return address.ToAddressDto();
        }

        public async Task<UserAddressDto> UpdateAddressAsync(Guid addressId, UpdateAddress update)
        {
            var address = await _userRepo.GetAddressByIdAsync(addressId)
                ?? throw new InvalidOperationException("Address does not exist");

            address.UpdateAddress(update);

            await _userRepo.SaveChangesAsync();

            return address.ToAddressDto();
        }
        public async Task<UserAddressDto> UpdateAddressDefaultAsync(Guid addressId)
        {
            var address = await _userRepo.GetAddressByIdAsync(addressId)
                ?? throw new InvalidOperationException("Address does not exist");

            if(address.IsDefault)
                throw new InvalidOperationException("This address is already the default one");

            var currentDefault = await _userRepo.GetDefaultUserAddressAsync(address.UserId);

            if(currentDefault !=null)
                currentDefault.IsDefault = false;

            address.IsDefault = true;

            await _userRepo.SaveChangesAsync();

            return address.ToAddressDto();
        }
    }
}