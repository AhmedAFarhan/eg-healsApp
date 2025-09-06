namespace EGHeals.Domain.ValueObjects
{
    public record TechnicianCost
    {
        public decimal Value { get; }
        private TechnicianCost(decimal value) => Value = value;

        public static TechnicianCost Of(decimal value)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);

            return new TechnicianCost(value);
        }
    }
}
