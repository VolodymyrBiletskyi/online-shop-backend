using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Users;

namespace api.Interfaces
{
    public interface IAuthService
    {
        Task<AuthWithRefreshToken?> LoginAsync(LoginUserDto login);
        Task<AuthWithRefreshToken?> RefeshAsync(string rawRefreshToken);
        Task LogoutAsync(Guid userId); 
    }
}