using BuildingBlocks.DataAccess.Contracts;
using BuildingBlocks.DataAccess.FakeDatabase;
using BuildingBlocks.DataAccessAbstraction.Models;
using BuildingBlocks.DataAccessAbstraction.Queries;
using BuildingBlocks.DataAccessAbstraction.Repository;
using BuildingBlocks.Domain.Abstractions;
using System.Linq.Expressions;

namespace BuildingBlocks.DataAccess.Repository
{
    public class BaseMocksRepository<T, TId>(DatabaseSimulator _db, IUserContext userContext) : IBaseRepository<T, TId> where T : SystemEntity<TId> where TId : class 
    {
        protected List<T> table  = _db.Set<T>();

        /************************************** Query methods ***************************************/

        public Task<IEnumerable<T>> GetAllAsync(QueryOptions<T> options, Expression<Func<T, object>>[]? includes = null, bool ignoreOwnership = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetCountAsync(QueryFilters<T> filters, bool ignoreOwnership = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //public Task<IEnumerable<T>> GetAllAsync(int pageIndex = 1,
        //                                        int pageSize = 50,
        //                                        IEnumerable<FilterCriteria>? filters = null,
        //                                        Expression<Func<T, object>>? orderBy = null,
        //                                        bool ascending = true,
        //                                        Expression<Func<T, object>>[]? includes = null,
        //                                        bool ignoreOwnership = false,
        //                                        CancellationToken cancellationToken = default)
        //{
        //    // for now: just simulate paging
        //    var result = table
        //        .Skip((pageIndex - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();

        //    return Task.FromResult<IEnumerable<T>>(result);
        //}

        public Task<T?> GetByIdAsync(TId id,
                                     Expression<Func<T, object>>[]? includes = null,
                                     bool ignoreOwnership = false,
                                     CancellationToken cancellationToken = default)
        {
            // assume every entity has an "Id" property
            var entity = table.FirstOrDefault(e =>
            {
                var prop = typeof(T).GetProperty("Id");
                return prop != null && prop.GetValue(e)?.Equals(id) == true;
            });

            return Task.FromResult(entity);
        }

        //public Task<long> GetCountAsync(IEnumerable<FilterCriteria>? filters = null,
        //                                bool ignoreOwnership = false, CancellationToken cancellationToken = default)
        //{
        //    return Task.FromResult((long)table.Count);
        //}

        /************************************** Command methods ***************************************/

        public Task<T> AddOneAsync(T entity, CancellationToken cancellationToken = default)
        {
            table.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            table.AddRange(entities);
            return Task.FromResult(entities);
        }

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public void HardDelete(T entity)
        {
            table.Remove(entity);
        }

        public void Update(T entity)
        {
            // remove old & add new (simple simulation)
            var prop = typeof(T).GetProperty("Id");
            if (prop != null)
            {
                var id = prop.GetValue(entity);
                var old = table.FirstOrDefault(e => prop.GetValue(e)?.Equals(id) == true);
                if (old != null) table.Remove(old);
            }

            table.Add(entity);
        }



        /************************************** Helper methods ***************************************/
    }
}
