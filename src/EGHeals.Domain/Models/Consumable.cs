namespace EGHeals.Domain.Models
{
    public class Consumable : Entity<ConsumableId>
    {
        internal Consumable(EncounterId encounterId, RadiologyItemId radiologyItemId, decimal qty)
        {
            Id = ConsumableId.Of(Guid.NewGuid());
            EncounterId = encounterId;
            RadiologyItemId = radiologyItemId;
            Qty = qty;
        }

        public EncounterId EncounterId { get; private set; } = default!;
        public RadiologyItemId RadiologyItemId { get; private set; } = default!;
        public decimal Qty { get; private set; } = default!;

        public void Update(decimal qty) => Qty = qty;
    }
}
