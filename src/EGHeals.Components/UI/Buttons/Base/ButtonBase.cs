using EGHeals.Models.Enums;
using Microsoft.AspNetCore.Components;

namespace EGHeals.Components.UI.Buttons.Base
{
    public abstract class ButtonBase : ComponentBase
    {
        [Parameter] public string Icon { get; set; } = default!;
        [Parameter] public bool IsDisabled { get; set; } = false;
        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter] public string? Class { get; set; }
        [Parameter] public ApplicationColor Color { get; set; } = ApplicationColor.PRIMARY;
        protected string GetVariantClass() => Color switch
        {
            ApplicationColor.PRIMARY => "button-primary",
            ApplicationColor.SECONDARY => "button-secondary",
            ApplicationColor.TERTIARY => "button-tertiary",
            _ => "button-primary"
        };
    }
}
