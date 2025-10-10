using BuildingBlocks.DataAccess.Contracts;
using BuildingBlocks.DataAccess.FakeDatabase;
using BuildingBlocks.DataAccess.PlatformTargets;
using BuildingBlocks.DataAccess.Repository;
using BuildingBlocks.DataAccess.UnitOfWork;
using BuildingBlocks.DataAccessAbstraction.Repository;
using BuildingBlocks.DataAccessAbstraction.UnitOfWork;
using BuildingBlocks.Domain.Abstractions;
using EGHeals.Application.Contracts.Users;
using EGHeals.Infrastructure.Data;
using EGHeals.Infrastructure.Data.Interceptors;
using EGHeals.Infrastructure.Helpers;
using EGHeals.Infrastructure.Repositories.Users;
using EGHeals.Infrastructure.Repositories.Users.Mocks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGHeals.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>(sp =>
            {
                return new AuditableEntityInterceptor(() => sp.GetRequiredService<IUserContext>());
            });

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(configuration.GetConnectionString("Database"));
            });

            services.AddSingleton<DatabaseSimulator>();

            services.AddTransient<DataBaseSetup>();

            services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();

            services.AddScoped<IUserRepository>(sp =>
            {
                var platform = sp.GetRequiredService<IPlatformTarget>();
                if (platform.Platform == PlatformType.MAUI)
                {
                    var db = sp.GetRequiredService<ApplicationDbContext>();
                    var userContext = sp.GetRequiredService<IUserContext>();
                    return new UserRepository<ApplicationDbContext>(db, userContext);
                }
                else
                {
                    var dbSimulator = sp.GetRequiredService<DatabaseSimulator>();
                    var userContext = sp.GetRequiredService<IUserContext>();
                    return new UserMockRepository(dbSimulator, userContext);
                }
            });

            return services;
        }
    }
}
