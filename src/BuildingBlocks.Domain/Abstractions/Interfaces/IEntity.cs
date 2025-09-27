using BuildingBlocks.Domain.ValueObjects;

namespace BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface IEntity<T> : IEntity, ISystemEntity<T> { }
    public interface IEntity : ISystemEntity
    {
        Guid CreatedBy { get; set; }
        Guid? LastModifiedBy { get; set; }
        Guid? DeletedBy { get; set; }
        SystemUserId OwnershipId { get; set; }
    }
}
