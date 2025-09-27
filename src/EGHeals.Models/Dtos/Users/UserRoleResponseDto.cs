namespace EGHeals.Models.Dtos.Users
{
    public record UserRoleResponseDto(Guid Id, string RoleName, IEnumerable<UserRolePermissionResponseDto> UserRolePermissions);

}
