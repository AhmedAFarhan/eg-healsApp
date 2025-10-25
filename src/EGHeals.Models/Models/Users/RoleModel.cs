using EGHeals.Models.Models.Users.Base;

namespace EGHeals.Models.Models.Users
{
    public class RoleModel : BaseRoleModel
    {
        public Guid UserRoleId { get; set; }
        public bool IsSelected { get; set; }
        public List<RolePermissionModel> RolePermissions { get; set; } = default!;
    }
}
