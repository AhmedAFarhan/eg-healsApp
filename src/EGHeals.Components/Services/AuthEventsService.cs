namespace EGHeals.Components.Services
{
    public class AuthEventsService
    {
        public event Action? OnUnauthorized;

        public void RaiseUnauthorized() => OnUnauthorized?.Invoke();
    }
}
