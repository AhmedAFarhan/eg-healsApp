using EGHeals.Services.Exceptions;

namespace EGHeals.Components.Services
{
    public class GlobalExceptionService
    {
        public event Action<string, string>? OnException;

        public void Handle(string errTitle, string errMsg) => OnException?.Invoke(errTitle, errMsg);
    }
}
