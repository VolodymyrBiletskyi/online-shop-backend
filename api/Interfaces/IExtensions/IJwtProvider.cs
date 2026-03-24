using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateAccesToken(User user);
        string GenerateRefreshTOken();
        string HashToken(string token);

        int AccessTokenMinutes { get; }
    }    
}