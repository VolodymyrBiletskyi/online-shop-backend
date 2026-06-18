using api.Modules.UserModule.DTOs.Requests;

namespace api.Modules.UserModule.Domain
{
    public interface IUserValidator
    {
        void ValidateCreateUser(CreateUserDto dto);
    }
}
