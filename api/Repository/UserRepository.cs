using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dto;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{


    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbContext.Users
            .AsNoTracking()
            .ToListAsync(ct);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
        }

        public Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, ct); 
        }

        public Task AddAsync(User entity, CancellationToken ct = default)
        {
            return _dbContext.AddAsync(entity, ct).AsTask();
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return _dbContext.SaveChangesAsync(ct);
        }

        
    }
}