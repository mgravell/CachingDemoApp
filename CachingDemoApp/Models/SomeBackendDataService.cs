using System.Collections.Immutable;

namespace CachingDemoApp.Models;

public class SomeBackendData
{
    public DateTime CreationTime { get; set; }
    public Guid Id { get; set; }
    public string? Provider { get; set; }
    // whatever other things we want to cache
}

public class SomeBackendDataService
{
    public virtual async Task<SomeBackendData> GetSomeExpensiveDataAsync(int id, CancellationToken cancellationToken)
        => await UnderlyingDataFetchAsync(id, cancellationToken);

    protected async ValueTask<SomeBackendData> UnderlyingDataFetchAsync(int id, CancellationToken cancellationToken)
    {
        // simulate an expensive fetch operation (in real code this code be database data, HTTP, gRPC, etc)
        _ = id;
        await Task.Delay(TimeSpan.FromMilliseconds(2500), cancellationToken);

        // invent some data to return
        return new SomeBackendData
        {
            CreationTime = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            Provider = GetType().Name,
        };
    }

    internal static TimeSpan Expiration = TimeSpan.FromSeconds(7.5);

    protected static ImmutableList<string> Tags { get; } = [ "general", "sales" ];
}
