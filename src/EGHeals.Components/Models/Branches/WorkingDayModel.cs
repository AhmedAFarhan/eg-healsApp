namespace EGHeals.Components.Models.Branches
{
    public class WorkingDayModel
    {
        public string DayName { get; set; } = default!;
        public int DayNum { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
