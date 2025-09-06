namespace BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface ISystemEntity<T> : ISystemEntity
    {
        T Id { get; set; }
    }
    public interface ISystemEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime? LastModifiedAt { get; set; }
        bool IsDeleted { get; set; }
    }
}
