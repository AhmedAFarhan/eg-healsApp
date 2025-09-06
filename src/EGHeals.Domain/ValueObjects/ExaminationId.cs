namespace EGHeals.Domain.ValueObjects
{
    public record ExaminationId
    {
        public Guid Value { get; }

        private ExaminationId(Guid value) => Value = value;

        public static ExaminationId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("ExaminationId can not be empty");
            }

            return new ExaminationId(value);
        }
    }
}
