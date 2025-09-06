namespace EGHeals.Domain.Models
{
    public class RadiologistDevice : Entity<RadiologistDeviceId>
    {
        internal RadiologistDevice(RadiologistId radiologistId, RadiologyDevice radiologyDevice)
        {
            Id = RadiologistDeviceId.Of(Guid.NewGuid());
            RadiologistId = radiologistId;
            RadiologyDevice = radiologyDevice;
        }

        public RadiologistId RadiologistId { get; private set; } = default!;
        public RadiologyDevice RadiologyDevice { get; private set; } = default!;
    }
}
