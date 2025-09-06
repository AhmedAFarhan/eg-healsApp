using Microsoft.AspNetCore.Components;

namespace EGHeals.Components.Services
{
    public class ModalPopupService
    {
        public event Action<RenderFragment, string?, string?>? OnShow;
        public event Func<bool>? OnClose;
        public event Action<bool>? OnStateChanged;

        public void Show(RenderFragment content, string? title = null, string? desc = null)
        {
            OnShow?.Invoke(content, title, desc);
            OnStateChanged?.Invoke(true);
        }

        public void Close()
        {
            var closed = OnClose?.Invoke();
            if (closed == true) OnStateChanged?.Invoke(false);
        }
    }
}
