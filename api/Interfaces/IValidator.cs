using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Contracts.Users.Request;

namespace api.Interfaces
{
    public interface IValidator
    {
        void ValidateCreateUser(CreateUserDto dto);
        
    }
}