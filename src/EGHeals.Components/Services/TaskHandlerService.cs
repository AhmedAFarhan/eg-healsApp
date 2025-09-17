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

        //public async Task<T?> RunAsync<T>(Func<Task<T>> taskFunc, Action<bool>? setLoading = null, Func<Exception, Task>? onError = null)
        //{
        //    try
        //    {
        //        setLoading?.Invoke(true);
        //        ErrorMessage = null;

        //        return await taskFunc();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMessage = ex.Message;
        //        if (onError is not null)
        //            await onError(ex);
        //        return default;
        //    }
        //    finally
        //    {
        //        setLoading?.Invoke(false);
        //    }
        //}
    }

}
