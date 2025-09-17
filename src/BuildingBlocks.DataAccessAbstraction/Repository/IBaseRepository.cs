using System.Linq.Expressions;

namespace BuildingBlocks.DataAccessAbstraction.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(int pageIndex = 1, int pageSize = 50, string? filterQuery = null, string? filterValue = null, Expression<Func<T, object>>[]? includes = null, CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync<TId>(TId id, Expression<Func<T, object>>[]? includes = null, CancellationToken cancellationToken = default);
        Task<T> AddOneAsync(T entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        void Delete(T entity);
        void Update(T entity);
        Task<long> GetCountAsync(string? filterQuery = null, string? filterValue = null, CancellationToken cancellationToken = default);       
    }
}
