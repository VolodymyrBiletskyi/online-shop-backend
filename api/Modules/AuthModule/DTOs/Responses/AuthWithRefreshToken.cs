namespace api.Modules.AuthModule.DTOs.Responses
{
    public class AuthWithRefreshToken
    {
        public AuthResult Auth { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
