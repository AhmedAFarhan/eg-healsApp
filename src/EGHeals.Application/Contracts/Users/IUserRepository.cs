
namespace EGHeals.Application.Contracts.Users
{
    public interface IUserRepository : IBaseRepository<SystemUser>
    {
        Task<SystemUser?> IsUserExistAsync(string username, CancellationToken cancellationToken = default);
        Task<SystemUser?> GetUserCredentialsAsync(string username, CancellationToken cancellationToken = default);
    }
}
