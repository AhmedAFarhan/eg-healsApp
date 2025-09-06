using BuildingBlocks.Domain.Abstractions.Interfaces;
using BuildingBlocks.Domain.ValueObjects;

namespace BuildingBlocks.Domain.Abstractions
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;
        public Guid CreatedBy { get; set; } = default!;
        public DateTime? LastModifiedAt { get; set; } = default!;
        public Guid? LastModifiedBy { get; set; } = default!;
        public OwnerId OwnerId { get; set; } = default!;
        public SystemUserId SystemUserId { get; set; } = default!;
        public bool IsDeleted { get; set; } = default!;
    }
}
