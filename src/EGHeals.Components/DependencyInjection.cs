using EGHeals.Components.Security;
using EGHeals.Components.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace EGHeals.Components
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddComponentsServices(this IServiceCollection services)
        {
            services.AddSingleton<LoadingService>();
            services.AddSingleton<ModalPopupService>();
            services.AddSingleton<GlobalExceptionService>();
            services.AddSingleton<TaskHandlerService>();

            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            return services;
        }
    }
}
