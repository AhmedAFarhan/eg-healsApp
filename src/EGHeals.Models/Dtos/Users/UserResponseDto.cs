namespace EGHeals.Models.Dtos.Users
{
    public record UserResponseDto(Guid Id, string Username, string? Email, string? Mobile, Guid OwnershipId, IEnumerable<UserRoleResponseDto> UserRoles);
}
