namespace EGHeals.Models.Dtos.Users.Responses
{
    public record SubUserResponseDto(Guid Id, string Username, IEnumerable<SubUserRoleResponseDto> UserRoles);
    
}
