namespace EGHeals.Application.Dtos.Users
{
    public record UserRoleDto(Guid Id, string RoleName, IEnumerable<UserRolePermissionDto> UserRolePermissions);
}
