using BuildingBlocks.DataAccessAbstraction.Repository;
using BuildingBlocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BuildingBlocks.DataAccess.Repository
{
    public class SystemRepository<T, TId, TContext>(TContext dbContext) : ISystemRepository<T, TId> where T : SystemEntity<TId> where TId : class where TContext : DbContext
    {
        private readonly DbSet<T> _dbSet = dbContext.Set<T>();

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>[]? includes = null, CancellationToken cancellationToken = default)
        {
            //Starting query
            var query = _dbSet.AsQueryable().Where(x => !x.IsDeleted);

            //Apply includes
            query = ApplyIncludes(query, includes);

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        private IQueryable<T> ApplyIncludes(IQueryable<T> query, Expression<Func<T, object>>[]? includes)
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
