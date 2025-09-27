
namespace EGHeals.Models.Dtos.Users
{
    public record SubUserResponseDto(Guid Id, string Username, IEnumerable<SubUserRoleResponseDto> UserRoles);
    
}
