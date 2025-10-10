using Blazored.LocalStorage;
using BuildingBlocks.DataAccess.Contracts;
using EGHeals.Components.Identity;
using EGHeals.Components.Security;
using EGHeals.Components.Security.Abstractions;
using EGHeals.Components.Services;
using EGHeals.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGHeals.Components
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddComponentsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBlazoredLocalStorage();

            services.AddSingleton<LoadingService>();
            services.AddSingleton<ModalPopupService>();
            services.AddSingleton<GlobalExceptionService>();
            services.AddSingleton<TaskHandlerService>();
            services.AddSingleton<MessageBoxService>();

            services.AddAuthorizationCore();
            services.AddScoped<CustomAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddScoped<ITokenStorageService, TokenStorageService>();
            services.AddScoped<IUserContext, UserContext>();

            services.AddInfrastructureServices(configuration);
            return services;
        }
    }
}
