using EGHeals.Application.Contracts.Roles;
using EGHeals.Application.Dtos.Roles;
using EGHeals.Application.Extensions.Roles;
using EGHeals.Domain.Enums;

namespace EGHeals.Application.Features.Users.Queries.GetRoles
{
    public class GetRolesQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetRolesQuery, EGResponse<IEnumerable<RoleDto>>>
    {
        public async Task<EGResponse<IEnumerable<RoleDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetCustomRepository<IRoleRepository>();

            var roles = await repo.GetRolesAsync(RoleType.RADIOLOGY, cancellationToken: cancellationToken);

            var rolesDtos = roles.ToRolesDtos();

            return new EGResponse<IEnumerable<RoleDto>>
            {
                Success = true,
                Data = rolesDtos
            };
        }
    }
}
