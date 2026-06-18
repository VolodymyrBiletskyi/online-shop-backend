namespace api.Modules.AuthModule.DTOs.Responses
{
    public class AuthResult
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string AccessToken { get; set; } = null!;
        public DateTime ExpiresAtUtc { get; set; }
    }
}
