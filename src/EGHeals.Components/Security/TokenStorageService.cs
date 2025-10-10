using Blazored.LocalStorage;
using EGHeals.Components.Security.Abstractions;
using EGHeals.Models.Dtos.Users.Responses;

namespace EGHeals.Components.Security
{
    public class TokenStorageService(ILocalStorageService localStorage) : ITokenStorageService
    {
        public async Task<UserResponseDto?> GetTokenAsync()
        {
           return await localStorage.GetItemAsync<UserResponseDto>("authToken");
        }

        public async Task SaveTokenAsync(UserResponseDto token) => await localStorage.SetItemAsync("authToken", token);

        public async Task RemoveTokenAsync() => await localStorage.RemoveItemAsync("authToken");        
    }
}
