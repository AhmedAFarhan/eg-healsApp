using EGHeals.Components.Security.Abstractions;
using EGHeals.Components.Services;
using System.Net.Http.Headers;

namespace EGHeals.Components.Security
{
    public class AuthMessageHandler : DelegatingHandler
    {
        private readonly ITokenStorageService _tokenStorage;
        private readonly CustomAuthStateProvider _customAuthStateProvider;
        private readonly AuthEventsService _authEventsService;

        public AuthMessageHandler(ITokenStorageService tokenStorage, CustomAuthStateProvider customAuthStateProvider, AuthEventsService authEventsService)
        {
            _tokenStorage = tokenStorage;
            _customAuthStateProvider = customAuthStateProvider;
            _authEventsService = authEventsService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenStorage.GetTokenAsync();

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _authEventsService.RaiseUnauthorized();
            }

            return response;
        }
    }
}
