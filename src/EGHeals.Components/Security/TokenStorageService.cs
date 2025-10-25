using Blazored.LocalStorage;
using EGHeals.Components.Security.Abstractions;

namespace EGHeals.Components.Security
{
    public class TokenStorageService(ILocalStorageService localStorage) : ITokenStorageService
    {
        public async Task<string?> GetTokenAsync() => await localStorage.GetItemAsync<string>("EGHeals_authToken");
        public async Task SaveTokenAsync(string token) => await localStorage.SetItemAsync("EGHeals_authToken", token);
        public async Task RemoveTokenAsync() => await localStorage.RemoveItemAsync("EGHeals_authToken");        
    }
}
