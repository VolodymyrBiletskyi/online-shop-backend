using System.IdentityModel.Tokens.Jwt;
using api.Extensions;
using api.Models;
using api.Modules.AuthModule.DTOs.Requests;
using api.Modules.AuthModule.DTOs.Responses;
using api.Modules.AuthModule.Jwt;
using api.Modules.AuthModule.Repository;
using api.Modules.UserModule.Repository;

namespace api.Modules.AuthModule.Domain
{
    public class AuthService : IAuthService
    {
        private readonly IJwtProvider _jwt;
        private readonly IUserRepository _userRepo;
        private readonly IAuthRepository _authRepo;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IJwtProvider jwt, IUserRepository userRepo, IAuthRepository authRepo,
            IPasswordHasher passwordHasher)
        {
            _jwt = jwt;
            _userRepo = userRepo;
            _authRepo = authRepo;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthWithRefreshToken?> LoginAsync(LoginUserDto login)
        {
            var user = await _userRepo.GetByEmailAsync(login.Email)
                ?? throw new InvalidOperationException("User not found");

            var result = _passwordHasher.Verify(login.Password, user.PasswordHash);

            if (result == false)
                throw new InvalidOperationException("Invalid Password");

            var accessToken = _jwt.GenerateAccessToken(user);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(accessToken);
            var expiresAt = DateTime.UtcNow.AddMinutes(_jwt.AccessTokenMinutes);

            var rawRefresh = _jwt.GenerateRefreshToken();
            var hash = _jwt.HashToken(rawRefresh);

            var refreshEntity = new RefreshToken
            {
                UserId = user.Id,
                TokenHash = hash,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _authRepo.AddAsync(refreshEntity);
            await _authRepo.SaveChangesAsync();

            var auth = new AuthResult
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                AccessToken = accessToken,
                ExpiresAtUtc = expiresAt
            };

            return new AuthWithRefreshToken
            {
                Auth = auth,
                RefreshToken = rawRefresh
            };
        }

        public async Task LogoutAsync(Guid userId)
        {
            var activeTokens = await _authRepo.GetActiveByUserAsync(userId);

            if (activeTokens.Count == 0)
                return;

            foreach (var token in activeTokens)
                token.RevokedAt = DateTime.UtcNow;

            await _authRepo.SaveChangesAsync();
        }

        public async Task<AuthWithRefreshToken?> RefreshAsync(string rawRefreshToken)
        {
            var hash = _jwt.HashToken(rawRefreshToken);
            var token = await _authRepo.GetByHashAsync(hash);

            if (token == null || !token.IsActive)
                return null;

            var user = await _userRepo.GetByIdAsync(token.UserId);
            if (user == null)
                return null;

            token.RevokedAt = DateTime.UtcNow;

            var newRawRefresh = _jwt.GenerateRefreshToken();
            var newHash = _jwt.HashToken(newRawRefresh);

            var newRefreshEntity = new RefreshToken
            {
                UserId = user.Id,
                TokenHash = newHash,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _authRepo.AddAsync(newRefreshEntity);
            await _authRepo.SaveChangesAsync();

            var newAccessToken = _jwt.GenerateAccessToken(user);
            var expiresAt = DateTime.UtcNow.AddMinutes(_jwt.AccessTokenMinutes);

            var auth = new AuthResult
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                AccessToken = newAccessToken,
                ExpiresAtUtc = expiresAt
            };

            return new AuthWithRefreshToken
            {
                Auth = auth,
                RefreshToken = newRawRefresh
            };
        }
    }
}
