using api.Modules.AuthModule.DTOs.Requests;
using api.Modules.AuthModule.DTOs.Responses;

namespace api.Modules.AuthModule.Domain
{
    public interface IAuthService
    {
        Task<AuthWithRefreshToken?> LoginAsync(LoginUserDto login);
        Task<AuthWithRefreshToken?> RefreshAsync(string rawRefreshToken);
        Task LogoutAsync(Guid userId);
    }
}
