using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace EGHeals.Components.UI.Inputs.Base
{
    public class InputBase<TValue> : ComponentBase
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        [Parameter]
        public string FontAwesomeIcon { get; set; } = default!;

        [Parameter]
        public string Label { get; set; } = default!;

        [Parameter]
        public string Placeholder { get; set; } = default!;

        [Parameter]
        public TValue Value { get; set; } = default!;

        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        [Parameter]
        public Expression<Func<TValue>> ValidationFor { get; set; } = default!;

        [CascadingParameter]
        protected EditContext CascadedEditContext { get; set; } = default!;

        protected bool ShowValidationErrors => ValidationMessages.Any();

        protected List<string> ValidationMessages { get; set; } = new();

        protected string ValidationClass => ShowValidationErrors ? "is-invalid" : "";

        protected TValue BindValue
        {
            get => Value;
            set
            {
                if (!EqualityComparer<TValue>.Default.Equals(Value, value))
                {
                    Value = value;
                    ValueChanged.InvokeAsync(value);
                    NotifyValidation();
                }
            }
        }

        protected override void OnInitialized()
        {
            if (CascadedEditContext != null)
            {
                CascadedEditContext.OnValidationStateChanged += ValidationStateChanged;
            }
        }

        protected void ValidationStateChanged(object sender, ValidationStateChangedEventArgs e)
        {
            var fieldIdentifier = FieldIdentifier.Create(ValidationFor);
            ValidationMessages = CascadedEditContext.GetValidationMessages(fieldIdentifier).ToList();
            InvokeAsync(StateHasChanged);
        }

        protected void NotifyValidation()
        {
            var fieldIdentifier = FieldIdentifier.Create(ValidationFor);
            CascadedEditContext?.NotifyFieldChanged(fieldIdentifier);
        }
    }
}
