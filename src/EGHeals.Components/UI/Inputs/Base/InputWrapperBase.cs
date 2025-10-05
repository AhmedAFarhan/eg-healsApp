using Microsoft.AspNetCore.Components;

namespace EGHeals.Components.UI.Inputs.Base
{
    public class InputWrapperBase : ComponentBase
    {

        [Parameter]
        public string Id { get; set; } = default!;

        [Parameter]
        public string Label { get; set; } = default!;

        [Parameter]
        public string FontAwesomeIcon { get; set; } = default!;

        [Parameter]
        public bool ShowValidationErrors { get; set; }

        [Parameter]
        public List<string> ValidationMessages { get; set; } = default!;

        [Parameter]
        public RenderFragment Icon { get; set; } = default!;

        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;

        [Parameter] public bool IsReadOnly { get; set; } = true;
    }
}
