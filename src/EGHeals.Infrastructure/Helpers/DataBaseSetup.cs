using EGHeals.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace EGHeals.Infrastructure.Helpers
{
    public class DataBaseSetup(ApplicationDbContext dbContext)
    {
        public async Task SetupAsync()
        {
            try
            {
                // Apply migrations
                await dbContext.Database.MigrateAsync();

                // Seed data if database is empty
                var existRoles = await dbContext.Roles.AnyAsync();

                if (!existRoles)
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

                    await dbContext.Roles.AddAsync(adminRole);
                    await dbContext.Roles.AddAsync(receptionistRole);
                    await dbContext.Roles.AddAsync(radiologistRole);
                    await dbContext.Roles.AddAsync(accountantRole);

                    await dbContext.SystemUsers.AddAsync(adminUser);
                    await dbContext.SystemUsers.AddAsync(subUser);

                    await dbContext.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
