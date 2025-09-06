using System.Linq.Expressions;

namespace BuildingBlocks.DataAccessAbstraction.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(int pageIndex = 1, int pageSize = 20, string? filterQuery = null, string? filterValue = null, Expression<Func<T, object>>[]? includes = null);
        Task<T?> GetByIdAsync(Guid id, Expression<Func<T, object>>[] includes = null);
        Task<T> AddOneAsync(T entity, Expression<Func<T, object>>[] includes = null);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<T?> UpdateAsync(Guid id, T entity, Expression<Func<T, object>>[] includes = null);
        Task<T?> DeleteAsync(Guid id);
        Task<long> GetCountAsync(string? filterQuery = null, string? filterValue = null);
        void UpdateEntity(T entity);
    }
}
