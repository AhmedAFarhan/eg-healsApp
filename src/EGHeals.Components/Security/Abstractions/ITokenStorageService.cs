using EGHeals.Models.Dtos.Users;

namespace EGHeals.Components.Security.Abstractions
{
    public interface ITokenStorageService
    {
        Task<UserResponseDto?> GetTokenAsync();
        Task SaveTokenAsync(UserResponseDto token);
        Task RemoveTokenAsync();
    }
}
