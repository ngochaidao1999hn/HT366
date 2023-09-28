using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Infrastructure.Services
{
    public interface ICachingService<T> where T : class
    {
        Task<IEnumerable<T>> GetData(string key);

        Task SetData(string key, byte[] data, DistributedCacheEntryOptions options);
    }
}