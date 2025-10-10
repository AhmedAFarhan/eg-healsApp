namespace EGHeals.Models.Dtos.Users.Responses
{
    public record UserRoleResponseDto(Guid Id, string RoleName, IEnumerable<UserRolePermissionResponseDto> UserRolePermissions);

}
