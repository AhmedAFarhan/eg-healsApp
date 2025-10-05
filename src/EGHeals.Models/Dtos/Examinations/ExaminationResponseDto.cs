namespace EGHeals.Models.Dtos.Examinations
{
    public class ExaminationResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Device { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public decimal Cost { get; set; }
    }
}
