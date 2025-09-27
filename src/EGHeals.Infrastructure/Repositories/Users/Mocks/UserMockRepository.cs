using BuildingBlocks.DataAccess.Contracts;
using BuildingBlocks.DataAccess.FakeDatabase;
using EGHeals.Application.Contracts.Users;
using System.Linq.Expressions;
using System.Reflection;

namespace EGHeals.Infrastructure.Repositories.Users.Mocks
{
    public class UserMockRepository : BaseMocksRepository<SystemUser, SystemUserId>, IUserRepository
    {
        private readonly DatabaseSimulator _db;
        public UserMockRepository(DatabaseSimulator db, IUserContext userContext) : base(db, userContext)
        {
            _db = db;
        }

        public async Task<SystemUser?> IsUserExistAsync(string username, CancellationToken cancellationToken = default)
        {
            if (username.Contains("@"))
            {
                //LOGIN WITH EMAIL
                return await Task.FromResult(table.FirstOrDefault(u => u.Email == username));
            }
            else if (username.All(char.IsDigit))
            {
                //LOGIN WITH MOBILE
                return await Task.FromResult(table.FirstOrDefault(u => u.Mobile == username));
            }
            else
            {
                //LOGIN WITH USERNAME
                return await Task.FromResult(table.FirstOrDefault(u => u.Username == username));
            }

        }

        public async Task<SystemUser?> GetUserCredentialsAsync(string username, CancellationToken cancellationToken = default)
        {
            // "Tables" from simulator
            var users = table;
            var roles = _db.Set<Role>();
            var rolePermissions = _db.Set<RolePermission>();

            // Find user
            var user = users.FirstOrDefault(u => u.Username == username && !u.IsDeleted);
            if (user == null)
            {
                return await Task.FromResult<SystemUser?>(null);
            }

            // Rebuild navigation properties manually
            foreach (var ur in user.UserRoles)
            {
                var role = roles.FirstOrDefault(r => r.Id == ur.RoleId);
                SetPrivateProperty(ur, nameof(ur.Role), role);

                foreach (var urp in ur.UserRolePermissions)
                {
                    var rolePermission = rolePermissions.FirstOrDefault(rp => rp.Id == urp.RolePermissionId);
                    SetPrivateProperty(urp, nameof(urp.RolePermission), rolePermission);
                }
            }

            return await Task.FromResult<SystemUser?>(user);
        }

        private void SetPrivateProperty<T>(object obj, string propertyName, T value)
        {
            var prop = obj.GetType().GetProperty(propertyName,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (prop == null) throw new InvalidOperationException($"Property {propertyName} not found");
            prop.SetValue(obj, value);
        }

        public Task<IEnumerable<SystemUser>> GetSubUsersByOwnershipAsync(int pageIndex = 1, int pageSize = 50, string? filterQuery = null, string? filterValue = null, bool ascending = true, Expression<Func<SystemUser, object>>? orderBy = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<SystemUser?> GetSubUserRolesByOwnershipAsync(Guid userId, Guid adminId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
