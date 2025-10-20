using EGHeals.Domain.Enums;
using EGHeals.Domain.ValueObjects;

namespace EGHeals.Application.Contracts.Roles
{
    public interface IRoleRepository : IBaseRepository<Role, RoleId>
    {
        Task<IEnumerable<Role>> GetRolesAsync(RoleType type, CancellationToken cancellationToken = default);
    }
}
