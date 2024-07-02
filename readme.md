# Basic `HybridCache` demo

License: MIT

This is a simple demo app that shows the .NET 9 `HybridCache` feature.

It is implemented as an asp.net MVC site. The interesting code is in /CachingDemoApp/Models/SomeBackendData.cs, which
has:

- an implementation without caching
- an implementation with manual `IDistributedCache` usage
- an implementation with `HybridCache` usage

These methods are used from /CachingDemoApp/Views/Home/Index.cshtml

Configuration is in /CachingDemoApp/Program.cs

Package addition is in /CachingDemoApp/CachingDemoApp.csproj


Key topics:

- ease of use
- stampede
- configuration
- serialization