using Microsoft.AspNetCore.Components;

namespace EGHeals.Components.Icons.Base
{
    public class IconBase : ComponentBase
    {
        [Parameter]
        public string ContainerClass { get; set; } = default!;

        [Parameter]
        public string PathClass { get; set; } = default!;
    }
}
