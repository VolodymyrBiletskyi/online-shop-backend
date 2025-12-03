using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _dbcontext;
        public RefreshTokenRepository(AppDbContext dbContext)
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