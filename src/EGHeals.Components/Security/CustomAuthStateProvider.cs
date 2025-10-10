using EGHeals.Components.Security.Abstractions;
using EGHeals.Models.Dtos.Users.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EGHeals.Components.Security
{
    public class CustomAuthStateProvider(ITokenStorageService tokenStorage) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = await tokenStorage.GetTokenAsync();

            if (user is null)
            {
                return new AuthenticationState(_anonymous);
            }

            var principal = BuildClaimsPrincipal(user);

            return new AuthenticationState(principal);
        }

        public async Task NotifyUserAuthentication(UserResponseDto user)
        {
            await tokenStorage.SaveTokenAsync(user);

            var principal = BuildClaimsPrincipal(user);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public async Task NotifyUserLogout()
        {
            await tokenStorage.RemoveTokenAsync();

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        /*****************************  HELPERS  ****************************/
        private ClaimsPrincipal BuildClaimsPrincipal(UserResponseDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            claims.Add(new Claim("OwnershipId", user.OwnershipId.ToString()));

            foreach (var role in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));

                foreach (var permission in role.UserRolePermissions)
                {
                    claims.Add(new Claim("Permission", permission.PermissionName));
                }
            }

            var identity = new ClaimsIdentity(claims, "CustomAuth");

            return new ClaimsPrincipal(identity);
        }
    }
}
