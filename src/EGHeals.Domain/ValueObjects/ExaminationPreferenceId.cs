namespace EGHeals.Domain.ValueObjects
{
    public record ExaminationPreferenceId
    {
        public Guid Value { get; }

        private ExaminationPreferenceId(Guid value) => Value = value;

        public static ExaminationPreferenceId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("ExaminationPreferenceId can not be empty");
            }

            return new ExaminationPreferenceId(value);
        }
    }
}
