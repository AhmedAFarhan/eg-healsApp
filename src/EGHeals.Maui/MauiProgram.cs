using EGHeals.Components;
using EGHeals.Components.Security;
using EGHeals.Services.ApiRequests;
using EGHeals.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace EGHeals.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add appsettings.json explicitly
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            builder.Services.AddMauiBlazorWebView();

            //builder.Services.AddSingleton<IPlatformTarget, PlatformTarget>();

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

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
