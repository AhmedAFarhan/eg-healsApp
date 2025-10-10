using BuildingBlocks.DataAccessAbstraction.Queries;
using BuildingBlocks.Domain.Abstractions.Interfaces;
using System.Linq.Expressions;


namespace BuildingBlocks.DataAccessAbstraction.Repository
{
    public interface ISystemRepository<T, TId> where T : ISystemEntity where TId : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>[]? includes = null,
                                         CancellationToken cancellationToken = default);

    }
}
