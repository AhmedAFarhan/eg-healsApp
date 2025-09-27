using BuildingBlocks.Domain.Abstractions.Interfaces;
using BuildingBlocks.Domain.Security;

namespace BuildingBlocks.Domain.Abstractions
{
    public class SystemEntity<T> : ISystemEntity<T>
    {
        public T Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;
        public DateTime? LastModifiedAt { get; set; } = default!;
        public DateTime? DeletedAt { get; set; } = default!;
        public bool IsDeleted { get; set; } = default!;
    }
}
