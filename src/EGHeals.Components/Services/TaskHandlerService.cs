using BuildingBlocks.Exceptions;

namespace EGHeals.Components.Services
{
    public class TaskHandlerService
    {
        public async Task RunAsync(Func<Task> taskFunc, Action? setLoading = null, Action? stopLoading = null, Action<AppException>? setError = null, Action<string>? setSuccess = null, string? SuccessMsg = null)
        {
            try
            {
                setError?.Invoke(null);

                setSuccess?.Invoke(string.Empty);

                setLoading?.Invoke();

                await Task.Delay(2000);

                await taskFunc();

                setSuccess?.Invoke(SuccessMsg);
            }
            catch (AppException ex)
            {
                setError?.Invoke(ex);
            }
            finally
            {
                stopLoading?.Invoke();               
            }
        }

        public async Task<T?> RunAsync<T>(Func<Task<T>> taskFunc, Action? setLoading = null, Action? stopLoading = null, Action<AppException>? setError = null, Action<string>? setSuccess = null, string? SuccessMsg = null)
        {
            try
            {
                setError?.Invoke(null);

                setSuccess?.Invoke(string.Empty);

                setLoading?.Invoke();

                await Task.Delay(2000);

                var result = await taskFunc();

                setSuccess?.Invoke(SuccessMsg);

                return result;
            }
            catch (AppException ex)
            {
                setError?.Invoke(ex);

                return default;
            }
            finally
            {
                stopLoading?.Invoke();               
            }
        }
    }

}
