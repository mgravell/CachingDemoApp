using Microsoft.Extensions.Caching.Memory;

namespace CachingDemoApp.Models;

public class WithMemoryCache(IMemoryCache memoryCache)
    : SomeBackendDataService
{
    public override async Task<SomeBackendData> GetSomeExpensiveDataAsync(
        int id, CancellationToken cancellationToken)
    {
        var key = $"/somepath/{id}";
        if (!(memoryCache.TryGetValue(key, out object? untyped)
            && untyped is SomeBackendData value))
        {
            // not available; go get it
            value = await UnderlyingDataFetchAsync(id, cancellationToken);
            memoryCache.Set(key, value, Expiration);
        }

        return value; // warning: shared copy (is it immutable?)
    }
}
