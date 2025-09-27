using BuildingBlocks.DataAccess.Contracts;
using EGHeals.Components.Security;
using System.Security.Claims;

namespace EGHeals.Components.Identity
{
    public class UserContext(CustomAuthStateProvider customAuthStateProvider) : IUserContext
    {
        public async Task<Guid> GetUserIdAsync()
        {
            var authState = await customAuthStateProvider.GetAuthenticationStateAsync();

            var user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var idValue = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (Guid.TryParse(idValue, out var userId))
                {
                    return userId;
                }
            }

            return Guid.Empty;
        }
        public async Task<Guid> GetOwnedByAsync()
        {
            var authState = await customAuthStateProvider.GetAuthenticationStateAsync();

            var user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var ownershipIdValue = user.FindFirst("OwnershipId")?.Value; // read the tenantId claim

                if (Guid.TryParse(ownershipIdValue, out var ownershipId))
                    return ownershipId;
            }

            return Guid.Empty; // return empty if claim not found or invalid
        }
    }
}
