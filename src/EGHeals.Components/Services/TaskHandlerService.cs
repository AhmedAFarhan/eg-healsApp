using EGHeals.Components.Models.Exceptions;

namespace EGHeals.Components.Services
{
    public class TaskHandlerService
    {
        public async Task RunAsync(Func<Task> taskFunc, Action? setLoading = null, Action? stopLoading = null, Action<CustomException>? setError = null, Action? setSuccess = null)
        {
            try
            {
                //setError?.Invoke(string.Empty);

                setLoading?.Invoke();

                await taskFunc();

                setSuccess?.Invoke();
            }
            catch (CustomException ex)
            {
                //var customEx = new CustomException {Title = "Error" ,Description = ex.Message };
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
