using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace api.Jwt
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        private readonly byte[] _key;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _key = Encoding.UTF8.GetBytes(_options.SecretKey);
        }
        public int AccessTokenMinutes => _options.AccesTokenMinutes;

        public string GenerateAccesToken(User user)
        {            
            Claim[] claims = [
                new("userId", user.Id.ToString()),
                new("email", user.Email ?? string.Empty)];

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_key),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            issuer: _options.Issuer,
            audience: _options.Audience,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_options.AccesTokenMinutes));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshTOken()
        {
            var bytes = new byte[64];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
        }

        public string HashToken(string token)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}