using BuildingBlocks.DataAccess.Repository;
using BuildingBlocks.DataAccessAbstraction.Repository;
using BuildingBlocks.DataAccessAbstraction.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.DataAccess.UnitOfWork
{
    public class UnitOfWork<TContext>(TContext dbContext, IServiceProvider serviceProvider) : IUnitOfWork where TContext : DbContext
    {
        private readonly Dictionary<Type, object> _repositories = new();

        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repository = new BaseRepository<T, TContext>(dbContext);
                _repositories[typeof(T)] = repository;
            }

            return (IBaseRepository<T>)_repositories[typeof(T)];
        }

        public TRepository GetCustomRepository<TRepository>() where TRepository : class
        {
            return serviceProvider.GetRequiredService<TRepository>();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
