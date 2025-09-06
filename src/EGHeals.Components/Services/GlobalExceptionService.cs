using EGHeals.Components.Models.Exceptions;

namespace EGHeals.Components.Services
{
    public class GlobalExceptionService
    {
        public event Action<CustomException>? OnException;

        public void Handle(CustomException ex) => OnException?.Invoke(ex);
    }
}
