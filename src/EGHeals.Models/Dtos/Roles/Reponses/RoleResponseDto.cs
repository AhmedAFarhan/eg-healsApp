namespace EGHeals.Models.Dtos.Roles.Reponses
{
    public record RoleResponseDto(Guid Id, string Name, IEnumerable<RolePermissionResponseDto> RolePermissions);
}
