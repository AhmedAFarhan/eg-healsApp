using BuildingBlocks.DataAccessAbstraction.Queries;

namespace EGHeals.Application.Contracts.Users
{
    public interface IUserRepository : IBaseRepository<SystemUser, SystemUserId>
    {
        Task<SystemUser?> IsUserExistAsync(string username, CancellationToken cancellationToken = default);

        Task<SystemUser?> GetUserCredentialsAsync(string username, CancellationToken cancellationToken = default);

        Task<IEnumerable<SystemUser>> GetSubUsersAsync(QueryOptions<SystemUser> options,
                                                                               bool ignoreOwnership = false,
                                                                               CancellationToken cancellationToken = default);

        Task<long> GetSubUsersCountAsync(QueryFilters<SystemUser> filters,
                                                                 bool ignoreOwnership = false,
                                                                 CancellationToken cancellationToken = default);

        Task<SystemUser?> GetSubUserRolesAsync(Guid userId,
                                               bool ignoreOwnership = false,
                                               CancellationToken cancellationToken = default);
    }
}
