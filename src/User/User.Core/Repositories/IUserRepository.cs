using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Core.Entities;

namespace User.Core.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserAsync(string username);
        Task<bool> IsUserExistAsync(string username);
        Task<bool> AddUserAsync(string username, string password);
        bool VerifyPassword(UserEntity userEntity, string password);
    }
}
