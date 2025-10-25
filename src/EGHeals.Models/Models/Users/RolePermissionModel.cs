using EGHeals.Models.Models.Users.Base;

namespace EGHeals.Models.Models.Users
{
    public class RolePermissionModel : BaseRoleModel
    {
        public Guid UserRolePermissionId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
