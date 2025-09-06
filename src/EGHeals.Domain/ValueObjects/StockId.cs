namespace EGHeals.Domain.ValueObjects
{
    public record StockId
    {
        public Guid Value { get; }

        private StockId(Guid value) => Value = value;

        public static StockId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("StockId can not be empty");
            }

            return new StockId(value);
        }
    }
}
