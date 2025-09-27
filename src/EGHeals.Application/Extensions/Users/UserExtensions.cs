using EGHeals.Application.Dtos.Users;
using System.Linq;

namespace EGHeals.Application.Extensions.Users
{
    public static class UserExtensions
    {
        public static UserDto ToUserDto(this SystemUser user)
        {
            return new UserDto
            (
                Id : user.Id.Value,
                Username : user.Username,
                Email : user.Email,
                Mobile : user.Mobile,
                OwnershipId: user.OwnershipId.Value,
                UserRoles : user.UserRoles.Select(role => new UserRoleDto
                (
                    Id : role.Id.Value,
                    RoleName : role.Role.Name,
                    UserRolePermissions : role.UserRolePermissions.Select(permission => new UserRolePermissionDto
                    (
                        Id : permission.Id.Value,
                        PermissionName : permission.RolePermission.RolePermissionType.ToString()
                    ))
                ))
            );
        }
        public static IEnumerable<SubUserDto> ToSubUsersDtos(this IEnumerable<SystemUser> users)
        {
            return users.Select(user => new SubUserDto
            (
                Id: user.Id.Value,
                Username: user.Username,
                UserRoles: user.UserRoles.Select(role => new SubUserRoleDto
                (
                    Id: role.Id.Value,
                    RoleName: role.Role.Name
                ))
            ));
        }
    }
}
