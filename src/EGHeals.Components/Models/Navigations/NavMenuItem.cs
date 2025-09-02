namespace EGHeals.Components.Models.Navigations
{
    public class NavMenuItem
    {
        public string Address { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Icon { get; set; } = default!;
        public bool IsActive { get; set; }
        public bool IsExpanded { get; set; }
        public int Counter { get; set; }
        public List<NavMenuItem> NavSubMenuItems { get; set; } = default!;
    }
}
