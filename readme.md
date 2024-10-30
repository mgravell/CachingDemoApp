# Basic `HybridCache` demo

License: MIT

This is a simple demo app that shows the .NET 9 `HybridCache` feature.

It is implemented as an asp.net MVC site. The interesting code is in `/CachingDemoApp/Models/`, which
has:

- an implementation without caching (`SomeBackendDataService.cs`)
- an implementation with manual `IMemoryCache` usage (`WithMemoryCache.cs`)
- an implementation with manual `IDistributedCache` usage (`WithDistributedCache.cs`)
- an implementation with `HybridCache` usage (`WithHybridCache.cs`)
- an implementation with zero-capture `HybridCache` usage (`WithHybridCacheNoCapture.cs`)

These methods are used from `/CachingDemoApp/Views/Home/Index.cshtml`

Configuration is in `/CachingDemoApp/Program.cs`

Package addition is in `/CachingDemoApp/CachingDemoApp.csproj`


Key topics:

- ease of use
- stampede
- L1/L2 differences
- configuration
- serialization
