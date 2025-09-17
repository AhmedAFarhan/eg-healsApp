using EGHeals.Application.Contracts.Users;

namespace EGHeals.Infrastructure.Repositories.Users
{
    public class UserRepository<TContext> : BaseRepository<SystemUser, TContext>, IUserRepository where TContext : DbContext
    {
        public UserRepository(TContext dbContext) : base(dbContext) { }

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
                               .FirstOrDefaultAsync(u => u.Username == username);
        }
       
    }
}
