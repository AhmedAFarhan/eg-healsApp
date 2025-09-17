using EGHeals.Components;
using EGHeals.Components.PlatformTargets;
using EGHeals.Maui.PlatformTargets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using EGHeals.Application;
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

            builder.Services.AddSingleton<IPlatformTarget, PlatformTarget>();

            builder.Services.AddApplicationServices();

            builder.Services.AddComponentsServices(builder.Configuration);

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
