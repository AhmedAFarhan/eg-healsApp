namespace EGHeals.Infrastructure.Data.Configurations
{
    public class SystemUserConfiguration : IEntityTypeConfiguration<SystemUser>
    {
        public void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => SystemUserId.Of(dbId));

            builder.Property(x => x.OwnershipId).HasConversion(id => id.Value, dbId => SystemUserId.Of(dbId));

            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Username).HasMaxLength(150).IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email).HasMaxLength(150).IsRequired(false);

            builder.HasIndex(x => x.Mobile).IsUnique();
            builder.Property(x => x.Mobile).HasMaxLength(11).IsRequired(false);

            builder.Property(x => x.UserType).HasDefaultValue(UserType.ADMIN).HasConversion(enums => enums.ToString(), dbEnums => (UserType)Enum.Parse(typeof(UserType), dbEnums));

            /*************************** Relationships ****************************/

            builder.HasMany(o => o.UserRoles).WithOne().HasForeignKey(tb => tb.SystemUserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<SystemUser>().WithMany().HasForeignKey(x => x.OwnershipId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
