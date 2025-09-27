using BuildingBlocks.DataAccess.PlatformTargets;
using EGHeals.Application;
using EGHeals.Application.Contracts.Users;
using EGHeals.Components;
using EGHeals.Components.Identity;
using EGHeals.Web;
using EGHeals.Web.PlatformTargets;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<IPlatformTarget, PlatformTarget>();

builder.Services.AddApplicationServices();

builder.Services.AddComponentsServices(builder.Configuration);

await builder.Build().RunAsync();
