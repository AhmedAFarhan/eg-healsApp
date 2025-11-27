using EGHeals.Components;
using EGHeals.Components.Security;
using EGHeals.Services.ApiRequests;
using EGHeals.Services.Services;
using EGHeals.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddComponentsServices();

builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<AuthMessageHandler>();
    handler.InnerHandler = new HttpClientHandler();

    return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:7080/")
    };
});

builder.Services.AddScoped<RequestHandler>();

builder.Services.AddScoped<EGService>();

//builder.Services.AddSingleton<IPlatformTarget, PlatformTarget>();

await builder.Build().RunAsync();
