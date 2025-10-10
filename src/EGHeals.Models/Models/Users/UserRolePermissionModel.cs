namespace EGHeals.Models.Models.Users
{
    public class UserRolePermissionModel
    {
        public string Permission { get; set; } = default!;
        public bool IsActive { get; set; } = false;
    }
}
