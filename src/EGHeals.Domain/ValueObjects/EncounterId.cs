namespace EGHeals.Domain.ValueObjects
{
    public record EncounterId
    {
        public Guid Value { get; }

        private EncounterId(Guid value) => Value = value;

        public static EncounterId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("EncounterId can not be empty");
            }

            return new EncounterId(value);
        }
    }
}
