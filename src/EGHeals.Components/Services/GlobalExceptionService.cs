using EGHeals.Models.Enums;

namespace EGHeals.Components.Services
{
    public class GlobalExceptionService
    {
        public event Action<string, string, ResponseType>? OnException;

        public void Handle(string errTitle, string errMsg, ResponseType responseType) => OnException?.Invoke(errTitle, errMsg, responseType);
    }
}
