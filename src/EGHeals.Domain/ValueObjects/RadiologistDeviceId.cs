namespace EGHeals.Domain.ValueObjects
{
    public record RadiologistDeviceId
    {
        public Guid Value { get; }

        private RadiologistDeviceId(Guid value) => Value = value;

        public static RadiologistDeviceId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologistDeviceId can not be empty");
            }

            return new RadiologistDeviceId(value);
        }
    }
}
