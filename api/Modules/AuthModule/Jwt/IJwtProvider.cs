using api.Models;

namespace api.Modules.AuthModule.Jwt
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        string HashToken(string token);
        int AccessTokenMinutes { get; }
    }
}
