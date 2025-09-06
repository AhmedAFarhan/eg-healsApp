namespace EGHeals.Domain.Models
{
    public class TechnicianExaminationCost : Entity<TechnicianExaminationCostId>
    {
        internal TechnicianExaminationCost(TechnicianId technicianId, ExaminationId examinationId, decimal cost)
        {
            Id = TechnicianExaminationCostId.Of(Guid.NewGuid());
            TechnicianId = technicianId;
            ExaminationId = examinationId;
            Cost = cost;
        }

        public TechnicianId TechnicianId { get; private set; } = default!;
        public ExaminationId ExaminationId { get; private set; } = default!;
        public decimal Cost { get; private set; } = default!;
    }
}
