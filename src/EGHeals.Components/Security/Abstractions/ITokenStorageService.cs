using EGHeals.Models.Dtos.Users.Responses;

namespace EGHeals.Components.Security.Abstractions
{
    public interface ITokenStorageService
    {
        Task<UserResponseDto?> GetTokenAsync();
        Task SaveTokenAsync(UserResponseDto token);
        Task RemoveTokenAsync();
    }
}
