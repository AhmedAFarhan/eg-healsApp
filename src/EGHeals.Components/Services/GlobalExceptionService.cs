
using BuildingBlocks.Exceptions;

namespace EGHeals.Components.Services
{
    public class GlobalExceptionService
    {
        public event Action<AppException>? OnException;

        public void Handle(AppException ex) => OnException?.Invoke(ex);
    }
}
