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
        [Parameter] public MainButtonVariant Variant { get; set; } = MainButtonVariant.PRIMARY;
        protected string GetVariantClass() => Variant switch
        {
            MainButtonVariant.PRIMARY => "button-primary",
            MainButtonVariant.SECONDARY => "button-secondary",
            MainButtonVariant.TERTIARY => "button-tertiary",
            _ => "button-primary"
        };
    }
}
