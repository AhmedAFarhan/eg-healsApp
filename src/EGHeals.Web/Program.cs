using EGHeals.Components;
using EGHeals.Components.Identity;
using EGHeals.Services.ApiRequests;
using EGHeals.Services.Services;
using EGHeals.Web;
using EGHeals.Web.PlatformTargets;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7080/") });

builder.Services.AddSingleton<RequestHandler>();

builder.Services.AddSingleton<EGService>();

//builder.Services.AddSingleton<IPlatformTarget, PlatformTarget>();

builder.Services.AddComponentsServices();

await builder.Build().RunAsync();
