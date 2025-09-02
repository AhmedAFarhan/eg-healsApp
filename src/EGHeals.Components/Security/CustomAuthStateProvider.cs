using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EGHeals.Components.Security
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly bool isAuthenticated = true;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (isAuthenticated)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Ahmed"),
                    new Claim(ClaimTypes.NameIdentifier, "1")
                };

                var identity = new ClaimsIdentity(claims, "CustomAuth");

                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            else
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
    }
}
