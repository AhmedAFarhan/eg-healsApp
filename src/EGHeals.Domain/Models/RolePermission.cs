namespace EGHeals.Domain.Models
{
    public class RolePermission : SystemEntity<RolePermissionId>
    {
        internal RolePermission(RoleId roleId, RolePermissionType rolePermissionType)
        {
            Id = RolePermissionId.Of(Guid.NewGuid());
            RoleId = roleId;
            RolePermissionType = rolePermissionType;
        }

        public RoleId RoleId { get; private set; } = default!;
        public RolePermissionType RolePermissionType { get; private set; } = default!;
    }
}
