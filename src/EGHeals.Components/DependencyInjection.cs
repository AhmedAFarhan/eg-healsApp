using Blazored.LocalStorage;
using EGHeals.Components.Security;
using EGHeals.Components.Security.Abstractions;
using EGHeals.Components.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace EGHeals.Components
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddComponentsServices(this IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();

            services.AddSingleton<LoadingService>();
            services.AddSingleton<ModalPopupService>();
            services.AddSingleton<GlobalExceptionService>();
            services.AddSingleton<TaskHandlerService>();
            services.AddSingleton<MessageBoxService>();
            services.AddSingleton<AuthEventsService>();

            services.AddAuthorizationCore();
            services.AddScoped<CustomAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddScoped<ITokenStorageService, TokenStorageService>();

            services.AddScoped<AuthMessageHandler>();

            return services;
        }
    }
}
