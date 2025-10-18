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

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            return await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id); 
        }

        public Task AddAsync(User entity)
        {
            return _dbContext.AddAsync(entity).AsTask();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<User?> DeleteAsync(Guid id)
        {
            var userModel = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userModel == null) return null;

            _dbContext.Users.Remove(userModel);
            await _dbContext.SaveChangesAsync();
            return userModel;
        }

        
    }
}