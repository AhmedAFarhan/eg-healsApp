namespace EGHeals.Models.Dtos.Users.Responses
{
    public record UserRoleResponseDto(Guid Id, Guid RoleId, string RoleName, IEnumerable<UserRolePermissionResponseDto> UserRolePermissions);
}
