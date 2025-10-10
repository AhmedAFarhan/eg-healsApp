using EGHeals.Application.Dtos.Roles;
using EGHeals.Application.Extensions.Roles;
using EGHeals.Domain.ValueObjects;

namespace EGHeals.Application.Features.Users.Queries.GetRoles
{
    public class GetRolesQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetRolesQuery, EGResponse<IEnumerable<RoleDto>>>
    {
        public async Task<EGResponse<IEnumerable<RoleDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetSystemRepository<Role, RoleId>();

            var roles = await repo.GetAllAsync(includes: [r => r.Permissions]);

            var rolesDtos = roles.ToRolesDtos();

            return new EGResponse<IEnumerable<RoleDto>>
            {
                Success = true,
                Data = rolesDtos
            };
        }
    }
}
