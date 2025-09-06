namespace EGHeals.Domain.ValueObjects
{
    public record ExaminationConsumablePreferenceId
    {
        public Guid Value { get; }

        private ExaminationConsumablePreferenceId(Guid value) => Value = value;

        public static ExaminationConsumablePreferenceId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("ExaminationConsumablePreferenceId can not be empty");
            }

            return new ExaminationConsumablePreferenceId(value);
        }
    }
}
