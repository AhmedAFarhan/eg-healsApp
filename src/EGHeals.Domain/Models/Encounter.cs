namespace EGHeals.Domain.Models
{
    public class Encounter : Aggregate<EncounterId>
    {
        private readonly List<Consumable> _consumables = new();
        public IReadOnlyList<Consumable> Consumables => _consumables.AsReadOnly();

        public PatientId PatientId { get; private set; } = default!;
        public ReferralDoctorId ReferralDoctorId { get; private set; } = default!;
        public RadiologistId RadiologistId { get; private set; } = default!;
        public TechnicianId TechnicianId { get; private set; } = default!;
        public ExaminationId ExaminationId { get; private set; } = default!;
        public RadiologyDevice RadiologyDevice { get; private set; } = default!;
        public EncounterStatus EncounterStatus { get; private set; } = default!;        
        public EncounterPricing EncounterPricing { get; private set; } = default!;
        public MedicalInsuranceId? MedicalInsuranceId { get; private set; } = default!;

        public static Encounter Create(PatientId patientId, ReferralDoctorId referralDoctorId, RadiologistId radiologistId, TechnicianId technicianId, ExaminationId examinationId, RadiologyDevice radiologyDevice, EncounterStatus encounterStatus, EncounterPricing encounterPricing, MedicalInsuranceId? medicalInsuranceId)
        {
            //Domain model validation
            Validation(radiologyDevice, encounterStatus);

            var encounter = new Encounter
            {
                Id = EncounterId.Of(Guid.NewGuid()),
                PatientId = patientId,
                ReferralDoctorId = referralDoctorId,
                RadiologistId = radiologistId,
                TechnicianId = technicianId,
                ExaminationId = examinationId,
                RadiologyDevice = radiologyDevice,
                EncounterStatus = encounterStatus,
                EncounterPricing = encounterPricing,
                MedicalInsuranceId = medicalInsuranceId,
            };

            return encounter;
        }
        public void Update(PatientId patientId, ReferralDoctorId referralDoctorId, RadiologistId radiologistId, TechnicianId technicianId, ExaminationId examinationId, RadiologyDevice radiologyDevice, EncounterStatus encounterStatus, EncounterPricing encounterPricing, MedicalInsuranceId? medicalInsuranceId)
        {
            //Domain model validation
            Validation(radiologyDevice, encounterStatus);

            PatientId = patientId;
            ReferralDoctorId = referralDoctorId;
            RadiologistId = radiologistId;
            TechnicianId = technicianId;
            ExaminationId = examinationId;
            RadiologyDevice = radiologyDevice;
            EncounterStatus = encounterStatus;
            EncounterPricing = encounterPricing;
            MedicalInsuranceId = medicalInsuranceId;
        }
        
        public void AddConsumable(RadiologyItemId radiologyItemId, decimal quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            var consumable = new Consumable(Id, radiologyItemId, quantity);

            _consumables.Add(consumable);
        }
        public void UpdateConsumable(RadiologyItemId radiologyItemId, decimal quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            var consumable = _consumables.FirstOrDefault(x => x.RadiologyItemId == radiologyItemId);

            if (consumable is not null)
            {
                consumable.Update(quantity);
            }
        }
        public void RemoveConsumable(RadiologyItemId radiologyItemId)
        {
            var consumable = _consumables.FirstOrDefault(x => x.RadiologyItemId == radiologyItemId);

            if (consumable is not null)
            {
                _consumables.Remove(consumable);
            }
        }

        private static void Validation(RadiologyDevice radiologyDevice, EncounterStatus encounterStatus)
        {
            if (!Enum.IsDefined<RadiologyDevice>(radiologyDevice))
            {
                throw new DomainException("radiologyDevice value is out of range");
            }
            if (!Enum.IsDefined<EncounterStatus>(encounterStatus))
            {
                throw new DomainException("encounterStatus value is out of range");
            }
        }
    }
}
