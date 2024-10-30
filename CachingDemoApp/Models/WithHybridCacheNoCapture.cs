using Microsoft.Extensions.Caching.Hybrid;

namespace CachingDemoApp.Models;

public class WithHybridCacheNoCapture(HybridCache hybridCache)
    : SomeBackendDataService
{
    public override async Task<SomeBackendData> GetSomeExpensiveDataAsync(
        int id, CancellationToken cancellationToken)
    {
        return await hybridCache.GetOrCreateAsync(
            $"/somepath/{id}", (id, obj: this),
            static (state, cancellation) => state.obj.UnderlyingDataFetchAsync(state.id, cancellation),
            cancellationToken: cancellationToken
        );
    }
}
