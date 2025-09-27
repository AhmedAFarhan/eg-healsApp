using BuildingBlocks.DataAccess.Contracts;
using BuildingBlocks.DataAccess.FakeDatabase;
using BuildingBlocks.DataAccess.PlatformTargets;
using BuildingBlocks.DataAccess.Repository;
using BuildingBlocks.DataAccessAbstraction.Repository;
using BuildingBlocks.DataAccessAbstraction.UnitOfWork;
using BuildingBlocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.DataAccess.UnitOfWork
{
    public class UnitOfWork<TContext>(TContext dbContext, DatabaseSimulator dbSimulator, IServiceProvider serviceProvider, IPlatformTarget? platformTarget = null, IUserContext? userContext = null) : IUnitOfWork where TContext : DbContext
    {
        private readonly Dictionary<Type, object> _repositories = new();

        public IBaseRepository<T, TId> GetRepository<T, TId>() where T : Entity<TId> where TId : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                IBaseRepository<T, TId> repository;

                if (platformTarget.Platform == PlatformType.MAUI)
                {
                    repository = new BaseRepository<T, TId, TContext>(dbContext, userContext);
                }
                else
                {
                    repository = new BaseMocksRepository<T, TId>(dbSimulator, userContext);
                }
               
                _repositories[typeof(T)] = repository;
            }

            return (IBaseRepository<T, TId>)_repositories[typeof(T)];
        }

        public TRepository GetCustomRepository<TRepository>() where TRepository : class
        {
            return serviceProvider.GetRequiredService<TRepository>();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (platformTarget.Platform == PlatformType.MAUI)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public void Dispose()
        {
            if (platformTarget.Platform == PlatformType.MAUI)
            {
                dbContext.Dispose();
            }                
        }
    }
}
