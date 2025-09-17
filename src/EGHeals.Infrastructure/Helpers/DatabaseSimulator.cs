using Microsoft.AspNetCore.Identity;

namespace EGHeals.Infrastructure.Helpers
{
    public class DatabaseSimulator
    {
        public List<SystemUser> SystemUsers = new();

        public List<Role> Roles = new();

        public List<RolePermission> RolePermissions = new();

        public List<UserRole> UserRoles = new();

        public List<UserRolePermission> UserRolePermissions = new();

        public void InitDb()
        {
            var passwordHasher = new PasswordHasher<SystemUser>();

            var adminRole = Role.Create("Admin");

            var receptionistRole = Role.Create("Receptionist");
            receptionistRole.AddPermission(RolePermissionType.READ);
            receptionistRole.AddPermission(RolePermissionType.DELETE);
            receptionistRole.AddPermission(RolePermissionType.WRITE);

            var radiologistRole = Role.Create("Radiologist");
            radiologistRole.AddPermission(RolePermissionType.READ);
            radiologistRole.AddPermission(RolePermissionType.DELETE);
            radiologistRole.AddPermission(RolePermissionType.WRITE);

            var accountantRole = Role.Create("Accountant");
            accountantRole.AddPermission(RolePermissionType.READ);
            accountantRole.AddPermission(RolePermissionType.DELETE);
            accountantRole.AddPermission(RolePermissionType.WRITE);

            var adminUser = SystemUser.Create("admin", null, null, passwordHasher.HashPassword(null, "010011012"), UserType.ADMIN, null);
            adminUser.AddUserRole(adminRole.Id);

            var subUser = SystemUser.Create("subuser", null, null, passwordHasher.HashPassword(null, "123"), UserType.SUBUSER, adminUser.Id);
            var userRole = subUser.AddUserRole(receptionistRole.Id);
            foreach (var permission in receptionistRole.Permissions)
            {
                userRole.AddPermission(permission.Id);
            }

            Roles.Add(adminRole);
            Roles.Add(receptionistRole);
            Roles.Add(radiologistRole);
            Roles.Add(accountantRole);

            SystemUsers.Add(adminUser);
            SystemUsers.Add(subUser);
        }
    }
}
