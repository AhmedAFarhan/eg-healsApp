using BuildingBlocks.DataAccess.UnitOfWork;
using BuildingBlocks.DataAccessAbstraction.UnitOfWork;
using EGHeals.Application.Contracts.Users;
using EGHeals.Infrastructure.Data;
using EGHeals.Infrastructure.Helpers;
using EGHeals.Infrastructure.Repositories.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGHeals.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>(sp =>
            //{
            //    return new AuditableEntityInterceptor(() => sp.GetRequiredService<IUserContext>());
            //});

            //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                //options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(configuration.GetConnectionString("Database"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();

            services.AddScoped<IUserRepository, UserRepository<ApplicationDbContext>>();

            services.AddSingleton<DatabaseSimulator>();

            services.AddTransient<DataBaseSetup>();

            return services;
        }
    }
}
