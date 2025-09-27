using BuildingBlocks.Domain.ValueObjects;
using System.Linq.Expressions;

namespace EGHeals.Application.Contracts.Users
{
    public interface IUserRepository : IBaseRepository<SystemUser, SystemUserId>
    {
        Task<SystemUser?> IsUserExistAsync(string username, CancellationToken cancellationToken = default);

        Task<SystemUser?> GetUserCredentialsAsync(string username, CancellationToken cancellationToken = default);

        Task<IEnumerable<SystemUser>> GetSubUsersByOwnershipAsync(int pageIndex = 1, int pageSize = 50, string? filterQuery = null, string? filterValue = null, bool ascending = true, Expression<Func<SystemUser, object>>? orderBy = null, CancellationToken cancellationToken = default);

        Task<SystemUser?> GetSubUserRolesByOwnershipAsync(Guid userId, Guid adminId, CancellationToken cancellationToken = default);
    }
}
