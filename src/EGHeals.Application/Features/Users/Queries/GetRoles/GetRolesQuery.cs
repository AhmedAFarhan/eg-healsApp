using EGHeals.Application.Dtos.Roles;

namespace EGHeals.Application.Features.Users.Queries.GetRoles
{
    public record GetRolesQuery() : IQuery<EGResponse<IEnumerable<RoleDto>>>;
}
