namespace EGHeals.Domain.Models
{
    public class Stock : Entity<StockId>
    {
        public RadiologyItemId RadiologyItemId { get; private set; } = default!;
        public StoreId StoreId { get; private set; } = default!;
        public decimal Qty { get; private set; }
        public decimal CriticalQty { get; private set; }

        public static Stock Create(RadiologyItemId radiologyItemId, StoreId storeId, decimal qty, decimal criticalQty)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(criticalQty, 0);

            var stock = new Stock
            {
                Id = StockId.Of(Guid.NewGuid()),
                RadiologyItemId = radiologyItemId,
                StoreId = storeId,
                Qty = qty,
                CriticalQty = criticalQty,
            };

            return stock;
        }
        public void Update(decimal qty, decimal criticalQty)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(criticalQty, 0);

            Qty = qty;
            CriticalQty = criticalQty;
        }
    }
}
