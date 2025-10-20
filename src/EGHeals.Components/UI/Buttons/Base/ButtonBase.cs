using EGHeals.Components.Enums;
using Microsoft.AspNetCore.Components;

namespace EGHeals.Components.UI.Buttons.Base
{
    public abstract class ButtonBase : ComponentBase
    {
        [Parameter] public string Icon { get; set; } = default!;
        [Parameter] public bool IsDisabled { get; set; } = false;
        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter] public string? Class { get; set; }
        [Parameter] public AppColors Color { get; set; } = AppColors.PRIMARY;
        protected string GetVariantClass() => Color switch
        {
            AppColors.PRIMARY => "button-primary",
            AppColors.SECONDARY => "button-secondary",
            AppColors.TERTIARY => "button-tertiary",
            _ => "button-primary"
        };
    }
}
