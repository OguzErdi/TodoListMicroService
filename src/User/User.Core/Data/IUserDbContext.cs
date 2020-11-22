using Couchbase;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace User.Core.Data
{
    public interface IUserDbContext
    {
        Task<IBucket> GetBucket();
    }
}
