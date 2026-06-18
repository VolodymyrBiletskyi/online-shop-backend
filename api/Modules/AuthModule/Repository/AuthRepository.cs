using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Modules.AuthModule.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _dbcontext;
        public AuthRepository(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public Task AddAsync(RefreshToken token)
        {
            return _dbcontext.AddAsync(token).AsTask();
        }

        public async Task<List<RefreshToken>> GetActiveByUserAsync(Guid userId)
        {
            var now = DateTime.UtcNow;
            return await _dbcontext.RefreshTokens
                .Where(t =>
                    t.UserId == userId &&
                    t.RevokedAt == null &&
                    t.ExpiresAt > now)
                .ToListAsync();
        }

        public async Task<RefreshToken?> GetByHashAsync(string tokenHash)
        {
            return await _dbcontext.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == tokenHash);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbcontext.SaveChangesAsync();
        }
    }
}
