using BuildingBlocks.DataAccess.FakeDatabase;
using BuildingBlocks.DataAccess.PlatformTargets;
using EGHeals.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EGHeals.Infrastructure.Helpers
{
    public class DataBaseSetup(ApplicationDbContext dbContext, DatabaseSimulator database, IPlatformTarget? platformTarget = null)
    {
        public async Task SetupAsync()
        {
            try
            {
                if (platformTarget.Platform == PlatformType.MAUI)
                {
                    await SetupSqlDatabase();
                }
                else
                {
                    SetupFakeDatabase();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private void SetupFakeDatabase()
        {
            /*  CRATING FAKE DATABASE  */

            database.Set<SystemUser>();
            database.Set<Role>();
            database.Set<RolePermission>();
            database.Set<UserRole>();
            database.Set<UserRolePermission>();

            /*  SEEDING SOME DATA  */

            var passwordHasher = new PasswordHasher<SystemUser>();

            var superAdminRole = Role.Create(RoleId.Of(Guid.NewGuid()), "SuperAdmin");

            var adminRole = Role.Create(RoleId.Of(Guid.NewGuid()), "RadiologistAdmin");

            var receptionistRole = Role.Create(RoleId.Of(Guid.NewGuid()), "Receptionist");
            receptionistRole.AddPermission(RolePermissionType.READ);
            receptionistRole.AddPermission(RolePermissionType.DELETE);
            receptionistRole.AddPermission(RolePermissionType.WRITE);

            var radiologistRole = Role.Create(RoleId.Of(Guid.NewGuid()), "Radiologist");
            radiologistRole.AddPermission(RolePermissionType.READ);
            radiologistRole.AddPermission(RolePermissionType.DELETE);
            radiologistRole.AddPermission(RolePermissionType.WRITE);

            var accountantRole = Role.Create(RoleId.Of(Guid.NewGuid()), "Accountant");
            accountantRole.AddPermission(RolePermissionType.READ);
            accountantRole.AddPermission(RolePermissionType.DELETE);
            accountantRole.AddPermission(RolePermissionType.WRITE);

            var superAdminUser = SystemUser.Create(SystemUserId.Of(Guid.NewGuid()), "superadmin", null, null, passwordHasher.HashPassword(null, "010011"), UserType.SUPER_ADMIN);
            superAdminUser.OwnershipId = superAdminUser.Id;
            var superAdminUserRole = superAdminUser.AddUserRole(superAdminRole.Id);
            superAdminUserRole.OwnershipId = superAdminUser.Id;

            var adminUser = SystemUser.Create(SystemUserId.Of(Guid.NewGuid()), "admin", null, null, passwordHasher.HashPassword(null, "010011012"), UserType.ADMIN);
            adminUser.OwnershipId = adminUser.Id;
            var adminUserRole = adminUser.AddUserRole(adminRole.Id);
            adminUserRole.OwnershipId = adminUser.Id;

            var subUser = SystemUser.Create(SystemUserId.Of(Guid.NewGuid()), "subuser", null, null, passwordHasher.HashPassword(null, "123"), UserType.SUBUSER);
            subUser.OwnershipId = adminUser.Id;
            var userRole = subUser.AddUserRole(receptionistRole.Id);
            userRole.OwnershipId = adminUser.Id;

            foreach (var permission in receptionistRole.Permissions)
            {
                var userRolePermission = userRole.AddPermission(permission.Id);
                userRolePermission.OwnershipId = adminUser.Id;
            }

            database.Insert<Role>(superAdminRole);
            database.Insert<Role>(adminRole);
            database.Insert<Role>(receptionistRole);
            database.Insert<Role>(radiologistRole);
            database.Insert<Role>(accountantRole);

            database.Insert<SystemUser>(superAdminUser);
            database.Insert<SystemUser>(adminUser);
            database.Insert<SystemUser>(subUser);

        }

        private async Task SetupSqlDatabase()
        {
            // Apply migrations
            await dbContext.Database.MigrateAsync();

            // Seed data if database is empty
            var existRoles = await dbContext.Roles.AnyAsync();

            if (!existRoles)
            {
                dbContext.IsSeeding = true;

                var passwordHasher = new PasswordHasher<SystemUser>();

                var superAdminRole = Role.Create(RoleId.Of(Guid.NewGuid()), "SuperAdmin");

                var adminRole = Role.Create(RoleId.Of(Guid.NewGuid()), "RadiologistAdmin");

                var receptionistRole = Role.Create(RoleId.Of(Guid.NewGuid()), "Receptionist");
                receptionistRole.AddPermission(RolePermissionType.READ);
                receptionistRole.AddPermission(RolePermissionType.DELETE);
                receptionistRole.AddPermission(RolePermissionType.WRITE);

                var radiologistRole = Role.Create(RoleId.Of(Guid.NewGuid()), "Radiologist");
                radiologistRole.AddPermission(RolePermissionType.READ);
                radiologistRole.AddPermission(RolePermissionType.DELETE);
                radiologistRole.AddPermission(RolePermissionType.WRITE);

                var accountantRole = Role.Create(RoleId.Of(Guid.NewGuid()), "Accountant");
                accountantRole.AddPermission(RolePermissionType.READ);
                accountantRole.AddPermission(RolePermissionType.DELETE);
                accountantRole.AddPermission(RolePermissionType.WRITE);

                var superAdminUser = SystemUser.Create(SystemUserId.Of(Guid.NewGuid()), "superadmin", null, null, passwordHasher.HashPassword(null, "010011"), UserType.SUPER_ADMIN);
                superAdminUser.OwnershipId = superAdminUser.Id; 
                var superAdminUserRole = superAdminUser.AddUserRole(superAdminRole.Id);
                superAdminUserRole.OwnershipId = superAdminUser.Id;

                var adminUser = SystemUser.Create(SystemUserId.Of(Guid.NewGuid()), "admin", null, null, passwordHasher.HashPassword(null, "010011012"), UserType.ADMIN);
                adminUser.OwnershipId = adminUser.Id;
                var adminUserRole = adminUser.AddUserRole(adminRole.Id);
                adminUserRole.OwnershipId = adminUser.Id;

                var subUser = SystemUser.Create(SystemUserId.Of(Guid.NewGuid()), "subuser", null, null, passwordHasher.HashPassword(null, "123"), UserType.SUBUSER);
                subUser.OwnershipId = adminUser.Id;
                var userRole = subUser.AddUserRole(receptionistRole.Id);
                userRole.OwnershipId = adminUser.Id;

                foreach (var permission in receptionistRole.Permissions)
                {
                    var userRolePermission = userRole.AddPermission(permission.Id);
                    userRolePermission.OwnershipId = adminUser.Id;
                }

                await dbContext.Roles.AddAsync(superAdminRole);
                await dbContext.Roles.AddAsync(adminRole);
                await dbContext.Roles.AddAsync(receptionistRole);
                await dbContext.Roles.AddAsync(radiologistRole);
                await dbContext.Roles.AddAsync(accountantRole);

                await dbContext.SystemUsers.AddAsync(superAdminUser);
                await dbContext.SystemUsers.AddAsync(adminUser);
                await dbContext.SystemUsers.AddAsync(subUser);

                await dbContext.SaveChangesAsync();

                dbContext.IsSeeding = false;
            }
        }
    }
}
