using Couchbase.Core.Exceptions.KeyValue;
using Couchbase.KeyValue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.Infrastructer.NamedProvider;
using User.Core.Data;
using User.Core.Entities;
using User.Core.PasswordHasher;
using User.Core.Repositories;

namespace User.Infrastructer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserBucketProvider userBucketProvider;
        private readonly IPasswordHasher passwordHasher;

        public UserRepository(IUserBucketProvider userBucketProvider, IPasswordHasher passwordHasher)
        {
            this.userBucketProvider = userBucketProvider;
            this.passwordHasher = passwordHasher;
        }

        public async Task<bool> AddUserAsync(string username, string password)
        {
            var collection = await GetCollectionAsync();

            string passwordHash = passwordHasher.HashPassword(password);

            var userEntity = new UserEntity();
            userEntity.Username = username;
            userEntity.PasswordHash = passwordHash;

            var result = await collection.UpsertAsync(username, userEntity);

            return result != null;
        }

        public async Task<UserEntity> GetUserAsync(string username)
        {
            var collection = await GetCollectionAsync();
            UserEntity userEntity;
            try
            {
                var result = await collection.GetAsync(username);
                userEntity = result.ContentAs<UserEntity>();
            }
            catch (DocumentNotFoundException)
            {
                return null;
            }

            return userEntity;
        }

        public bool VerifyPassword(UserEntity userEntity, string password)
        {
            var isPasswordCorrect =  passwordHasher.VerifyPassword(password, userEntity.PasswordHash);

            return isPasswordCorrect;
        }

        public async Task<bool> IsUserExistAsync(string username)
        {
            var userEntity = await GetUserAsync(username);

            return userEntity != null;
        }

        private async Task<ICouchbaseCollection> GetCollectionAsync()
        {
            var result = await userBucketProvider.GetBucketAsync();

            return result.DefaultCollection();
        }
    }
}
