namespace EGHeals.Models.Models.Users.NewFolder
{
    public class BaseRoleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ActivationBtnText { get; set; } = "Activate";
        public string ActivationBtnIcon { get; set; } = "fa-sharp fa-solid fa-lightbulb-on";
        public bool IsActive { get; set; } = false;

        public void ToggleActivation()
        {
            // Toggle role activation
            IsActive = !IsActive;

            ActivationBtnText = IsActive ? "Deactivate" : "Activate";

            ActivationBtnIcon = IsActive ? "fa-sharp fa-solid fa-lightbulb-slash" : "fa-sharp fa-solid fa-lightbulb-on";
        }
    }
}
