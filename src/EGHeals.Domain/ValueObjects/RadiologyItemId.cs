namespace EGHeals.Domain.ValueObjects
{
    public record RadiologyItemId
    {
        public Guid Value { get; }

        private RadiologyItemId(Guid value) => Value = value;

        public static RadiologyItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologyItemId can not be empty");
            }

            return new RadiologyItemId(value);
        }
    }
}
