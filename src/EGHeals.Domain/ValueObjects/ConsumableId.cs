namespace EGHeals.Domain.ValueObjects
{
    public record ConsumableId
    {
        public Guid Value { get; }

        private ConsumableId(Guid value) => Value = value;

        public static ConsumableId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("ConsumableId can not be empty");
            }

            return new ConsumableId(value);
        }
    }
}
