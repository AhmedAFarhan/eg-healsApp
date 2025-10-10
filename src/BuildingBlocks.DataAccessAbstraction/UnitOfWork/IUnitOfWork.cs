using BuildingBlocks.DataAccessAbstraction.Repository;
using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.DataAccessAbstraction.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<T, TId> GetRepository<T, TId>() where T : Entity<TId> where TId : class;
        ISystemRepository<T, TId> GetSystemRepository<T, TId>() where T : SystemEntity<TId> where TId : class;
        TRepository GetCustomRepository<TRepository>() where TRepository : class;
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
