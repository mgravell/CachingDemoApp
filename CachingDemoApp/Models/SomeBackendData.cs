using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Hybrid;
using System.Text.Json;

namespace CachingDemoApp.Models
{
    public class SomeBackendData
    {
        public DateTime CreationTime { get; set; }
        public Guid Id { get; set; }
        // whatever other things we want to cache
    }

    public class SomeBackendDataService
        //(IDistributedCache distributedCache)
        //(HybridCache hybridCache)

    {
        private static async Task<SomeBackendData> GetSomeExpensiveDataAsync(CancellationToken cancellationToken)
        {
            // simulate an expensive fetch operation (in real code this code be database data, HTTP, gRPC, etc)
            await Task.Delay(TimeSpan.FromMilliseconds(2500), cancellationToken);

            // invent some data to return
            return new SomeBackendData
            {
                CreationTime = DateTime.UtcNow,
                Id = Guid.NewGuid(),
            };
        }

        public Task<SomeBackendData> GetSomeExpensiveDataWithoutCacheAsync(CancellationToken cancellationToken)
            => GetSomeExpensiveDataAsync(cancellationToken);

        #region Manual caching

        //public async Task<SomeBackendData> GetSomeExpensiveDataWithManualCachingAsync(CancellationToken cancellationToken)
        //{
        //    string key = "my expensive data key"; // could also include parameter values, i.e. $"/foos/{region}/{id}"
        //    var bytes = await distributedCache.GetAsync(key, cancellationToken);
        //    if (bytes is null)
        //    {
        //        // fetch from the backend
        //        var data = await GetSomeExpensiveDataAsync(cancellationToken);

        //        // serialize and cache for future callers
        //        using var ms = new MemoryStream();
        //        JsonSerializer.Serialize(ms, data);
        //        await distributedCache.SetAsync(key, ms.ToArray(), DistributedCacheTimeout, cancellationToken);

        //        // return the data we fetched from the backend
        //        return data;
        //    }
        //    else
        //    {
        //        // deserialize and return the cached data
        //        return JsonSerializer.Deserialize<SomeBackendData>(bytes)!;
        //    }
        //}

        //// usually this would be longer; short timeout is so we can see it expire during execution
        //private static readonly DistributedCacheEntryOptions DistributedCacheTimeout = new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(15) };

        #endregion Manual caching

        #region Hybrid caching

        //public ValueTask<SomeBackendData> GetSomeExpensiveDataWithHybridCachingAsync(CancellationToken cancellationToken)
        //    => hybridCache.GetOrCreateAsync<SomeBackendData>("my expensive data key", ct => new(GetSomeExpensiveDataAsync(ct)),
        //            HybridCacheTimeout, token: cancellationToken);

        //// usually this would be longer; short timeout is so we can see it expire during execution
        //private static readonly HybridCacheEntryOptions HybridCacheTimeout = new() {
        //    Expiration = TimeSpan.FromSeconds(15), LocalCacheExpiration = TimeSpan.FromSeconds(15) };

        #endregion
    }
}
