namespace BuildingBlocks.DataAccess.Contracts
{
    public interface IUserContext
    {
        Task<Guid> GetUserIdAsync();
        Task<Guid> GetOwnedByAsync();
    }
}
