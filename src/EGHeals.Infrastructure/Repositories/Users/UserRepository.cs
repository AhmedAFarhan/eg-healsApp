using BuildingBlocks.DataAccess.Contracts;
using BuildingBlocks.DataAccess.Helpers;
using EGHeals.Application.Contracts.Users;
using System.Linq;
using System.Linq.Expressions;

namespace EGHeals.Infrastructure.Repositories.Users
{
    public class UserRepository<TContext> : BaseRepository<SystemUser, SystemUserId, TContext>, IUserRepository where TContext : DbContext
    {
        public UserRepository(TContext dbContext, IUserContext userContext) : base(dbContext, userContext) { }

        public async Task<SystemUser?> IsUserExistAsync(string username, CancellationToken cancellationToken = default)
        {
            if (username.Contains("@"))
            {
                //LOGIN WITH EMAIL
                return await _dbSet.FirstOrDefaultAsync(u => u.Email == username);
            }
            else if (username.All(char.IsDigit))
            {
                //LOGIN WITH MOBILE
                return await _dbSet.FirstOrDefaultAsync(u => u.Mobile == username);
            }
            else
            {
                //LOGIN WITH USERNAME
                return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
            }
        }

        public async Task<SystemUser?> GetUserCredentialsAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking()
                               .AsSplitQuery()
                               .Include(x => x.UserRoles)
                                    .ThenInclude(x => x.Role)
                               .Include(x => x.UserRoles)
                                    .ThenInclude(x => x.UserRolePermissions)
                                        .ThenInclude(x => x.RolePermission)
                               .FirstOrDefaultAsync(u => u.Username == username && !u.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<SystemUser>> GetSubUsersByOwnershipAsync(int pageIndex = 1,
                                                                              int pageSize = 50,
                                                                              string? filterQuery = null,
                                                                              string? filterValue = null,
                                                                              bool ascending = true,
                                                                              Expression<Func<SystemUser, object>>? orderBy = null,
                                                                              CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable().Where(x => !x.IsDeleted);

            //Apply Ownership
            query = await ApplyOwnership(query, false);

            query = query.Include(x => x.UserRoles)
                            .ThenInclude(x => x.Role);

            //Filtering
            var filterExpression = DynamicFilter.BuildDynamicFilter<SystemUser>(filterValue, string.IsNullOrWhiteSpace(filterQuery) ? "all" : filterQuery);

            query = filterExpression is null ? query : query.Where(filterExpression);

            // Ordering
            query = orderBy != null ? (ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy)) : query.OrderBy(x => x.CreatedAt);

            //Paginations
            query = query.OrderBy(x => x.CreatedAt).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return await query.AsNoTracking().AsSplitQuery().ToListAsync(cancellationToken);
        }

        public async Task<SystemUser?> GetSubUserRolesByOwnershipAsync(Guid userId, Guid adminId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                                             .AsSplitQuery().
                                             Include(x => x.UserRoles)
                                                .ThenInclude(x => x.Role)
                                            .Include(x => x.UserRoles)
                                                .ThenInclude(x => x.UserRolePermissions)
                                                    .ThenInclude(x => x.RolePermission)
                                            .FirstOrDefaultAsync(x => x.Id == SystemUserId.Of(userId) /*&& x.OwnedBy == SystemUserId.Of(adminId)*/ && !x.IsDeleted);

        }
    }
}
