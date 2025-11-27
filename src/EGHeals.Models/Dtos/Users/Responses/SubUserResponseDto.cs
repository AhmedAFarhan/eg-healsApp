namespace EGHeals.Models.Dtos.Users.Responses
{
    public record SubUserResponseDto(Guid Id, string FirstName, string LastName, string Username, string? Email, string? PhoneNumber, IEnumerable<SubUserRoleResponseDto> Roles);
}
