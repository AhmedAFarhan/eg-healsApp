namespace EGHeals.Domain.ValueObjects
{
    public record ReferralDoctorCost
    {
        public decimal Value { get; }
        private ReferralDoctorCost(decimal value) => Value = value;

        public static ReferralDoctorCost Of(decimal value)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);

            return new ReferralDoctorCost(value);
        }
    }
}
