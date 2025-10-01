using BuildingBlocks.DataAccess.Contracts;
using BuildingBlocks.DataAccess.FakeDatabase;
using BuildingBlocks.DataAccessAbstraction.Queries;
using EGHeals.Application.Contracts.Users;
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

        public async Task<IEnumerable<SystemUser>> GetSubUsersAsync(QueryOptions<SystemUser> options, bool ignoreOwnership = false, CancellationToken cancellationToken = default)
        {
            var returnedUsers = new List<SystemUser>();
            var roles = _db.Set<Role>();
            var rolePermissions = _db.Set<RolePermission>();

            foreach (var user in table.ToList())
            {
                foreach (var userRole in user.UserRoles)
                {
                    var role = roles.FirstOrDefault(r => r.Id == userRole.RoleId);
                    SetPrivateProperty(userRole, nameof(userRole.Role), role);
                    foreach (var userPermission in userRole.UserRolePermissions)
                    {
                        var rolePermission = rolePermissions.FirstOrDefault(rp => rp.Id == userPermission.RolePermissionId);
                        SetPrivateProperty(userPermission, nameof(userPermission.RolePermission), rolePermission);
                    }
                }

                returnedUsers.Add(user);
            }

            return await Task.FromResult<IEnumerable<SystemUser>>(returnedUsers);
        }

        public async Task<long> GetSubUsersCountAsync(QueryFilters<SystemUser> filters, bool ignoreOwnership = false, CancellationToken cancellationToken = default)
        {
            var count = table.ToList().Count();
            return await Task.FromResult<long>(count);
        }

        public async Task<SystemUser?> GetSubUserRolesAsync(Guid userId, bool ignoreOwnership, CancellationToken cancellationToken)
        {
            // "Tables" from simulator
            var users = table;
            var roles = _db.Set<Role>();
            var rolePermissions = _db.Set<RolePermission>();

            // Find user
            var user = users.FirstOrDefault(u => u.Id == SystemUserId.Of(userId) && !u.IsDeleted);
            if (user == null)
            {
                return await Task.FromResult<SystemUser?>(null);
            }

            return await Task.FromResult<SystemUser?>(user);
        }

    }
}
