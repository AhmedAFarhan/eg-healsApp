namespace EGHeals.Components.Security.Abstractions
{
    public interface ITokenStorageService
    {
        Task<string?> GetTokenAsync();
        Task SaveTokenAsync(string token);
        Task RemoveTokenAsync();
    }
}
