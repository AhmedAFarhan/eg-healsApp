namespace EGHeals.Domain.Models
{
    public class ExaminationConsumablePreference : Entity<ExaminationConsumablePreferenceId>
    {
        internal ExaminationConsumablePreference(ExaminationPreferenceId examinationPreferenceId, RadiologyItemId radiologyItemId, decimal qty)
        {
            Id = ExaminationConsumablePreferenceId.Of(Guid.NewGuid());
            ExaminationPreferenceId = examinationPreferenceId;
            RadiologyItemId = radiologyItemId;
            Qty = qty;
        }

        public ExaminationPreferenceId ExaminationPreferenceId { get; private set; } = default!;
        public RadiologyItemId RadiologyItemId { get; private set; } = default!;
        public decimal Qty { get; private set; } = default!;

        public void Update(decimal qty) => Qty = qty;
    }
}
