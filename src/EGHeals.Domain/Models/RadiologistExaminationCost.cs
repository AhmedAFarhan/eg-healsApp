namespace EGHeals.Domain.Models
{
    public class RadiologistExaminationCost : Entity<RadiologistExaminationCostId>
    {
        internal RadiologistExaminationCost(RadiologistId radiologistId, ExaminationId examinationId, decimal cost)
        {
            Id = RadiologistExaminationCostId.Of(Guid.NewGuid());
            RadiologistId = radiologistId;
            ExaminationId = examinationId;
            Cost = cost;
        }

        public RadiologistId RadiologistId { get; private set; } = default!;
        public ExaminationId ExaminationId { get; private set; } = default!;
        public decimal Cost { get; private set; } = default!;
    }
}
