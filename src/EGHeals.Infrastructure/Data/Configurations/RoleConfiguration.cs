namespace EGHeals.Infrastructure.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => RoleId.Of(dbId));

            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();

            builder.Property(x => x.RoleType).HasDefaultValue(RoleType.NONE).HasConversion(enums => enums.ToString(), dbEnums => (RoleType)Enum.Parse(typeof(RoleType), dbEnums));

            builder.HasIndex(x => new { x.Name , x.RoleType }).IsUnique();

            /*************************** Relationships ****************************/

            builder.HasMany(o => o.Permissions).WithOne().HasForeignKey(tb => tb.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
