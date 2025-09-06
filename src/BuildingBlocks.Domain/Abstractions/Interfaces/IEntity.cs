using BuildingBlocks.Domain.ValueObjects;

namespace BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface IEntity<T> : IEntity, ISystemEntity<T> { }
    public interface IEntity : ISystemEntity
    {
        Guid CreatedBy { get; set; }
        Guid? LastModifiedBy { get; set; }
        OwnerId OwnerId { get; set; }
        SystemUserId SystemUserId { get; set; }
    }
}
