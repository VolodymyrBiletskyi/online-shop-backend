namespace api.Extensions
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string Password) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(Password);

        public bool Verify(string password, string hashedPassword) =>
            BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        
    }
}