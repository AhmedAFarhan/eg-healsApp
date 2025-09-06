namespace EGHeals.Domain.Models
{
    public class TechnicianDevice : Entity<TechnicianDeviceId>
    {
        internal TechnicianDevice(TechnicianId technicianId, RadiologyDevice radiologyDevice)
        {
            Id = TechnicianDeviceId.Of(Guid.NewGuid());
            TechnicianId = technicianId;
            RadiologyDevice = radiologyDevice;
        }

        public TechnicianId TechnicianId { get; private set; } = default!;
        public RadiologyDevice RadiologyDevice { get; private set; } = default!;
    }
}
