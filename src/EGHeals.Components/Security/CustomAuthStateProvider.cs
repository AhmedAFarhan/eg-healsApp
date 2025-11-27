using EGHeals.Components.Security.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EGHeals.Components.Security
{
    public class CustomAuthStateProvider(ITokenStorageService tokenStorage, NavigationManager navigation) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await tokenStorage.GetTokenAsync();

            if (token is null || IsTokenExpired(token))
            {
                return new AuthenticationState(_anonymous);
            }

            var principal = BuildClaimsPrincipal(token);

            return new AuthenticationState(principal);
        }

        public async Task NotifyUserAuthentication(string token)
        {
            await tokenStorage.SaveTokenAsync(token);

            var principal = BuildClaimsPrincipal(token);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public async Task NotifyUserLogout()
        {
            await tokenStorage.RemoveTokenAsync();

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));

            // Redirect to login
            navigation.NavigateTo("/", forceLoad: false);
        }

        public bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            // exp is in UTC
            return jwt.ValidTo < DateTime.UtcNow;
        }
        
        /*****************************  HELPERS  ****************************/
        private ClaimsPrincipal BuildClaimsPrincipal(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenClaims = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(tokenClaims.Claims, "jwt");

            return new ClaimsPrincipal(identity);
        }

        
    }
}
