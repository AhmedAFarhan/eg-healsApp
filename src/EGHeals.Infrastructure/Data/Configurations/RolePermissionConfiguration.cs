namespace EGHeals.Infrastructure.Data.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => RolePermissionId.Of(dbId));

            builder.Property(x => x.RoleId).HasConversion(id => id.Value, dbId => RoleId.Of(dbId));

            builder.Property(x => x.RolePermissionType).HasDefaultValue(RolePermissionType.READ).HasConversion(enums => enums.ToString(), dbEnums => (RolePermissionType)Enum.Parse(typeof(RolePermissionType), dbEnums));

            builder.HasIndex(x => new { x.RoleId, x.RolePermissionType }).IsUnique();
        }
    }
}
