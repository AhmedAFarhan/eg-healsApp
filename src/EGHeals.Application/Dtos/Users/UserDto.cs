namespace EGHeals.Application.Dtos.Users
{
    public record UserDto(Guid Id, string Username, string? Email, string? Mobile, Guid OwnershipId, IEnumerable<UserRoleDto> UserRoles);
}
