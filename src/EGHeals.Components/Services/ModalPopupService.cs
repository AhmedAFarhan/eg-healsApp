using Microsoft.AspNetCore.Components;

namespace EGHeals.Components.Services
{
    public class ModalPopupService
    {
        private TaskCompletionSource<object?>? _tcs;

        public event Action<RenderFragment, string?, string?>? OnShow;
        public event Func<RenderFragment, string?, string?, Task>? OnShowDialog;
        public event Func<bool>? OnClose;
        public event Action<bool>? OnStateChanged;

        public void Show(RenderFragment content, string? title = null, string? desc = null)
        {
            OnShow?.Invoke(content, title, desc);
            OnStateChanged?.Invoke(true);
        }
        public Task<T?> ShowDialog<T>(RenderFragment content, string? title = null, string? desc = null)
        {
            _tcs = new TaskCompletionSource<object?>();
            OnShowDialog?.Invoke(content, title, desc);
            OnStateChanged?.Invoke(true);
            return _tcs.Task.ContinueWith(t => (T?)t.Result);
        }
        public void Close(object? result = null)
        {
            if (OnClose?.Invoke() is true)
            {
                _tcs?.TrySetResult(result);
                OnStateChanged?.Invoke(false);
            }
        }
    }
}
