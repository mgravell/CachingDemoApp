using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;

namespace CachingDemoApp.Controllers;

public class AdminController(HybridCache cache) : Controller
{
    public async Task<string> Invalidate(string tag)
    {
        await cache.RemoveByTagAsync(tag);
        return $"Invalidated tag: '{tag}'";
    }
}
