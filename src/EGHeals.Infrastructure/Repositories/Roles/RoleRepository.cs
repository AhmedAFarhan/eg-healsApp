using BuildingBlocks.DataAccess.Contracts;
using EGHeals.Application.Contracts.Roles;

namespace EGHeals.Infrastructure.Repositories.Roles
{
    public class RoleRepository<TContext> : BaseRepository<Role, RoleId, TContext>, IRoleRepository where TContext : DbContext
    {
        public RoleRepository(TContext dbContext, IUserContext userContext) : base(dbContext, userContext) { }

        public async Task<IEnumerable<Role>> GetRolesAsync(RoleType type, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsQueryable()
                               .AsNoTracking()
                               .Include(x => x.Permissions)
                                    .ThenInclude(x => x.Permission)
                               .Where(x => !x.IsDeleted && x.IsActive && x.RoleType == type)
                               .ToListAsync(cancellationToken);

        }
    }
}
