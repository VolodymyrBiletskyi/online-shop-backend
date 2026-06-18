using api.Models;

namespace api.Modules.AuthModule.Repository
{
    public interface IAuthRepository
    {
        Task<RefreshToken?> GetByHashAsync(string tokenHash);
        Task AddAsync(RefreshToken token);
        Task<List<RefreshToken>> GetActiveByUserAsync(Guid userId);
        Task<int> SaveChangesAsync();
    }
}
