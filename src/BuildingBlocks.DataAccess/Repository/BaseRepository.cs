using BuildingBlocks.DataAccess.Helpers;
using BuildingBlocks.DataAccessAbstraction.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BuildingBlocks.DataAccess.Repository
{
    public class BaseRepository<T, TContext>(TContext dbContext) : IBaseRepository<T> where T : class where TContext : DbContext
    {
        protected readonly DbSet<T> _dbSet = dbContext.Set<T>();

        public async Task<IEnumerable<T>> GetAllAsync(int pageIndex = 1, int pageSize = 50, string? filterQuery = null, string? filterValue = null, Expression<Func<T, object>>[]? includes = null, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();

            //Apply includes
            query = ApplyIncludes(query, includes);

            //Filtering
            var filterExpression = DynamicFilter.BuildDynamicFilter<T>(filterValue, string.IsNullOrWhiteSpace(filterQuery) ? "all" : filterQuery);

            query = filterExpression is null ? query : query.Where(filterExpression);

            //Paginations
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync<TId>(TId id, Expression<Func<T, object>>[]? includes = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            query = ApplyIncludes(query, includes);

            var key = await _dbSet.FindAsync(id, cancellationToken);
            if (key == null) return null;

            // EF doesn't allow includes with FindAsync, so reload entity with includes
            var keyValues = dbContext.Entry(key).Metadata.FindPrimaryKey()!.Properties
                                     .Select(p => dbContext.Entry(key).Property(p.Name).CurrentValue)
                                     .ToArray();

            return await query.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<object>(e, "Id")!.Equals(keyValues[0]), cancellationToken);
        }

        public async Task<T> AddOneAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            return entities;
        }

        public void Delete(T entity) => _dbSet.Remove(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public async Task<long> GetCountAsync(string? filterQuery = null, string? filterValue = null, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();

            //Filtering
            var filterExpression = DynamicFilter.BuildDynamicFilter<T>(filterValue, string.IsNullOrWhiteSpace(filterQuery) ? "all" : filterQuery);

            query = filterExpression is null ? query : query.Where(filterExpression);

            return await query.LongCountAsync(cancellationToken);
        }


        /************************************** Helper methods ***************************************/

        private static IQueryable<T> ApplyIncludes(IQueryable<T> query, Expression<Func<T, object>>[]? includes)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.AsSplitQuery().Include(include);
                }
            }
            return query;
        }

    }
}
