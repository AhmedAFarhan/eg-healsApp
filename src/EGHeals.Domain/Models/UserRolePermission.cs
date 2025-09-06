namespace EGHeals.Domain.Models
{
    public class UserRolePermission : SystemEntity<UserRolePermissionId>
    {
        internal UserRolePermission(UserRoleId userRoleId, RolePermissionId rolePermissionId)
        {
            Id = UserRolePermissionId.Of(Guid.NewGuid());
            UserRoleId = userRoleId;
            RolePermissionId = rolePermissionId;
        }

        public UserRoleId UserRoleId { get; private set; } = default!;
        public RolePermissionId RolePermissionId { get; private set; } = default!;
    }
}
