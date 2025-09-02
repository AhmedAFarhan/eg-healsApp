using EGHeals.Components.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace EGHeals.Components
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddComponentsServices(this IServiceCollection services)
        {
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            return services;
        }
    }
}
