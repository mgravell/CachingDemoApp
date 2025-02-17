using Microsoft.Extensions.Caching.Hybrid;

namespace CachingDemoApp.Models;

public class WithHybridCache(HybridCache hybridCache)
    : SomeBackendDataService
{
    public override async Task<SomeBackendData> GetSomeExpensiveDataAsync(
        int id, CancellationToken cancellationToken)
    {
        return await hybridCache.GetOrCreateAsync(
            $"/somepath/{id}",
            cancellation => UnderlyingDataFetchAsync(id, cancellation),
            tags: Tags,
            cancellationToken: cancellationToken
        );
    }
}
