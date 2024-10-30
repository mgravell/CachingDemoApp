using CachingDemoApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(setup =>
{
    setup.Configuration = "127.0.0.1:6379";
});
#pragma warning disable EXTEXP0018 // experimental (pre-release)
builder.Services.AddHybridCache(options =>
{
    options.DefaultEntryOptions = new() {
        LocalCacheExpiration = SomeBackendDataService.Expiration,
        Expiration = SomeBackendDataService.Expiration,
    };
});
#pragma warning restore EXTEXP0018

// CHANGE HERE to see the behaviour of different cache implementations
builder.Services.AddSingleton<SomeBackendDataService>();
//builder.Services.AddSingleton<SomeBackendDataService, WithMemoryCache>();
//builder.Services.AddSingleton<SomeBackendDataService, WithDistributedCache>();
//builder.Services.AddSingleton<SomeBackendDataService, WithHybridCache>();
//builder.Services.AddSingleton<SomeBackendDataService, WithHybridCacheNoCapture>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
