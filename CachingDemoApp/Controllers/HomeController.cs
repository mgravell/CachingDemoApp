using CachingDemoApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CachingDemoApp.Controllers
{
    public class HomeController(SomeBackendDataService backend) : Controller
    {
        public async Task<IActionResult> IndexAsync(CancellationToken cancellation)
        {
            ViewData.Add("clock", DateTime.UtcNow);
            var timer = Stopwatch.StartNew();

            var data = await backend.GetSomeExpensiveDataWithoutCacheAsync(cancellation);
            //var data = await backend.GetSomeExpensiveDataWithManualCachingAsync(cancellation);
            //var data = await backend.GetSomeExpensiveDataWithHybridCachingAsync(cancellation);

            timer.Stop();
            ViewData.Add("timer", timer.ElapsedMilliseconds);
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
