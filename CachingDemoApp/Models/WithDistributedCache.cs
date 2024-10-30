using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CachingDemoApp.Models;

public class WithDistributedCache(IDistributedCache distributedCache)
    : SomeBackendDataService
{
    public override async Task<SomeBackendData> GetSomeExpensiveDataAsync(
        int id, CancellationToken cancellationToken)
    {
        var key = $"/somepath/{id}";
        var arr = await distributedCache.GetAsync(key, cancellationToken);

        SomeBackendData value;
        if (arr is null)
        {
            // not available; get and store
            value = await UnderlyingDataFetchAsync(id, cancellationToken);
            arr = SomeSerializer.Serialize(value);
            await distributedCache.SetAsync(key, arr,
                OptionsWithExpiration, cancellationToken);
        }
        else
        {
            // available; rehydrate
            value = SomeSerializer.Deserialize<SomeBackendData>(arr);
        }
        return value;
    }

    private static DistributedCacheEntryOptions OptionsWithExpiration
        => new() { AbsoluteExpirationRelativeToNow = Expiration };

    static class SomeSerializer
    {
        public static byte[] Serialize<T>(T value)
            => JsonSerializer.SerializeToUtf8Bytes(value);

        public static T Deserialize<T>(byte[] value)
            => JsonSerializer.Deserialize<T>(value)!;
    }
}
