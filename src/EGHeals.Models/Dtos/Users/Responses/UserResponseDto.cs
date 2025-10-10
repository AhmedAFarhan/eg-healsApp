namespace EGHeals.Models.Dtos.Users.Responses
{
    public record UserResponseDto(Guid Id, string Username, string? Email, string? Mobile, Guid OwnershipId, IEnumerable<UserRoleResponseDto> UserRoles);
}
