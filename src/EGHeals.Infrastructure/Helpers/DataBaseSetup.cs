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


            var permission1Id = PermissionId.Of(Guid.NewGuid());
            var permission1 = Permission.Create(permission1Id, "Permission 1");

            var permission2Id = PermissionId.Of(Guid.NewGuid());
            var permission2 = Permission.Create(permission1Id, "Permission 3");

            var permission3Id = PermissionId.Of(Guid.NewGuid());
            var permission3 = Permission.Create(permission1Id, "Permission 3");

            var permission4Id = PermissionId.Of(Guid.NewGuid());
            var permission4 = Permission.Create(permission1Id, "Permission 4");

            var superAdminRoleId = RoleId.Of(Guid.NewGuid());
            var superAdminRole = Role.Create(superAdminRoleId, "Super Admin", RoleType.NONE);

            var adminRoleId = RoleId.Of(Guid.NewGuid());
            var adminRole = Role.Create(adminRoleId, "Radiologist Admin", RoleType.NONE);

            var receptionistRoleId = RoleId.Of(Guid.NewGuid());
            var receptionistRole = Role.Create(receptionistRoleId, "Receptionist", RoleType.RADIOLOGY);

            receptionistRole.AddPermission(permission1Id);
            receptionistRole.AddPermission(permission2Id);

            var radiologistRoleId = RoleId.Of(Guid.NewGuid());
            var radiologistRole = Role.Create(radiologistRoleId, "Radiologist", RoleType.RADIOLOGY);

            radiologistRole.AddPermission(permission1Id);
            radiologistRole.AddPermission(permission1Id);

            var accountantRoleId = RoleId.Of(Guid.NewGuid());
            var accountantRole = Role.Create(accountantRoleId, "Accountant", RoleType.RADIOLOGY);

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

                var permission1Id = PermissionId.Of(Guid.NewGuid());
                var permission1 = Permission.Create(permission1Id, "Permission 1");

                var permission2Id = PermissionId.Of(Guid.NewGuid());
                var permission2 = Permission.Create(permission2Id, "Permission 2");

                var permission3Id = PermissionId.Of(Guid.NewGuid());
                var permission3 = Permission.Create(permission3Id, "Permission 3");

                var permission4Id = PermissionId.Of(Guid.NewGuid());
                var permission4 = Permission.Create(permission4Id, "Permission 4");

                var superAdminRoleId = RoleId.Of(Guid.NewGuid());
                var superAdminRole = Role.Create(superAdminRoleId, "Super Admin", RoleType.NONE);

                var adminRoleId = RoleId.Of(Guid.NewGuid());
                var adminRole = Role.Create(adminRoleId, "Radiologist Admin", RoleType.NONE);

                var receptionistRoleId = RoleId.Of(Guid.NewGuid());
                var receptionistRole = Role.Create(receptionistRoleId, "Receptionist", RoleType.RADIOLOGY);

                receptionistRole.AddPermission(permission1Id);
                receptionistRole.AddPermission(permission2Id);

                var radiologistRoleId = RoleId.Of(Guid.NewGuid());
                var radiologistRole = Role.Create(radiologistRoleId, "Radiologist", RoleType.RADIOLOGY);

                radiologistRole.AddPermission(permission3Id);
                radiologistRole.AddPermission(permission4Id);

                var accountantRoleId = RoleId.Of(Guid.NewGuid());
                var accountantRole = Role.Create(accountantRoleId, "Accountant", RoleType.RADIOLOGY);

                var superAdminUser = SystemUser.Create(SystemUserId.Of(Guid.NewGuid()), "superAdmin", null, null, passwordHasher.HashPassword(null, "010011"), UserType.SUPER_ADMIN);
                superAdminUser.OwnershipId = superAdminUser.Id; 
                var superAdminUserRole = superAdminUser.AddUserRole(superAdminRole.Id);
                superAdminUserRole.OwnershipId = superAdminUser.Id;

                var adminUser = SystemUser.Create(SystemUserId.Of(Guid.NewGuid()), "admin", null, null, passwordHasher.HashPassword(null, "010011"), UserType.ADMIN);
                adminUser.OwnershipId = adminUser.Id;
                var adminUserRole = adminUser.AddUserRole(adminRole.Id);
                adminUserRole.OwnershipId = adminUser.Id;

                var subUser = SystemUser.Create(SystemUserId.Of(Guid.NewGuid()), "subuser", null, null, passwordHasher.HashPassword(null, "010011"), UserType.SUBUSER);
                subUser.OwnershipId = adminUser.Id;
                var userRole = subUser.AddUserRole(receptionistRole.Id);
                userRole.OwnershipId = adminUser.Id;

                foreach (var permission in receptionistRole.Permissions)
                {
                    var userRolePermission = userRole.AddPermission(permission.Id);
                    userRolePermission.OwnershipId = adminUser.Id;
                }

                await dbContext.Permissions.AddAsync(permission1);
                await dbContext.Permissions.AddAsync(permission2);
                await dbContext.Permissions.AddAsync(permission3);
                await dbContext.Permissions.AddAsync(permission4);

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
