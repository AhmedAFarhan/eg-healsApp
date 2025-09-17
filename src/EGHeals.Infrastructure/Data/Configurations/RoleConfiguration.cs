namespace EGHeals.Infrastructure.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => RoleId.Of(dbId));

            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();

            /*************************** Relationships ****************************/

            builder.HasMany(o => o.Permissions).WithOne().HasForeignKey(tb => tb.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
