namespace EGHeals.Infrastructure.Data.Configurations
{
    public class UserRolePermissionConfiguration : IEntityTypeConfiguration<UserRolePermission>
    {
        public void Configure(EntityTypeBuilder<UserRolePermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => UserRolePermissionId.Of(dbId));

            builder.Property(x => x.OwnershipId).HasConversion(id => id.Value, dbId => SystemUserId.Of(dbId));

            builder.Property(x => x.UserRoleId).HasConversion(id => id.Value, dbId => UserRoleId.Of(dbId));

            builder.Property(x => x.RolePermissionId).HasConversion(id => id.Value, dbId => RolePermissionId.Of(dbId));

            /*************************** Relationships ****************************/

            //builder.HasOne<RolePermission>().WithMany().HasForeignKey(x => x.RolePermissionId).OnDelete(DeleteBehavior.Restrict); // incase we don't need navigation property

            builder.HasOne<SystemUser>().WithMany().HasForeignKey(x => x.OwnershipId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
