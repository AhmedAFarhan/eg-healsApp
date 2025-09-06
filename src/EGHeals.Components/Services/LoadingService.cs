namespace EGHeals.Components.Services
{
    public class LoadingService
    {
        public event Action<bool>? OnStateChanged;

        public void Show() => OnStateChanged?.Invoke(true);
        public void Hide() => OnStateChanged?.Invoke(false);
    }

}
