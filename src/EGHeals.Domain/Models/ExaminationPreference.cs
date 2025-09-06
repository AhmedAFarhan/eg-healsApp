namespace EGHeals.Domain.Models
{
    public class ExaminationPreference : Aggregate<ExaminationPreferenceId>
    {
        private readonly List<ExaminationConsumablePreference> _consumablePreferences = new();

        public IReadOnlyList<ExaminationConsumablePreference> ConsumablePreferences => _consumablePreferences.AsReadOnly();

        public string Name { get; private set; } = default!;
        public ExaminationId ExaminationId { get; private set; } = default!;

        public static ExaminationPreference Create(string name, ExaminationId examinationId)
        {
            Validation(name);

            var examinationPreference = new ExaminationPreference
            {
                Id = ExaminationPreferenceId.Of(Guid.NewGuid()),
                Name = name,
            };

            return examinationPreference;
        }
        public void Update(string name, ExaminationId examinationId)
        {
            Validation(name);

            Name = name;
        }

        public void AddConsumablePreference(RadiologyItemId radiologyItemId, decimal qty)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(qty);

            var consumable = new ExaminationConsumablePreference(Id, radiologyItemId, qty);

            _consumablePreferences.Add(consumable);
        }
        public void UpdateConsumablePreference(RadiologyItemId radiologyItemId, decimal quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            var consumable = _consumablePreferences.FirstOrDefault(x => x.RadiologyItemId == radiologyItemId);

            if (consumable is not null)
            {
                consumable.Update(quantity);
            }
        }
        public void RemoveConsumablePreference(RadiologyItemId radiologyItemId)
        {
            var consumable = _consumablePreferences.FirstOrDefault(x => x.RadiologyItemId == radiologyItemId);

            if (consumable is not null)
            {
                _consumablePreferences.Remove(consumable);
            }
        }

        private static void Validation(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, 3);
        }
    }
}
