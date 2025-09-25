using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default);
        Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default); 
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
          
        Task AddAsync(User entity, CancellationToken ct = default);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        
    }
}