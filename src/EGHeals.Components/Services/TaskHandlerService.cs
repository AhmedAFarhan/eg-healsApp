using EGHeals.Models.Exceptions;
using EGHeals.Services.Responses;

namespace EGHeals.Components.Services
{
    public class TaskHandlerService
    {
        public async Task<EGResponse<string>> RunAsync(Func<Task> taskFunc)
        {
            try
            {
                await Task.Delay(2000);

                await taskFunc();

                return new EGResponse<string>
                {
                    Success = true,
                    Message = "Success Operation."
                };
            }
            catch(Exception ex)
            {
                return new EGResponse<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                };
            }
            finally
            {

            }
        }
        public async Task<EGResponse<string>> RunAsync(Func<Task> taskFunc, Action onStart, Action onFinish)
        {
            try
            {
                onStart.Invoke();

                await Task.Delay(2000);

                await taskFunc();

                return new EGResponse<string>
                {
                    Success = true,
                    Message = "Success Operation."
                };
            }
            catch (Exception ex)
            {
                return new EGResponse<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                };
            }
            finally
            {
                onFinish.Invoke();
            }
        }
        public async Task<EGResponse<T>> RunAsync<T>(Func<Task<EGResponse<T>>> taskFunc)
        {
            try
            {
                await Task.Delay(2000);

                var result = await taskFunc();

                return result;
            }
            catch (Exception ex)
            {
                return new EGResponse<T>
                {
                    Success = false,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                };
            }
            finally
            {

            }
        }
        public async Task<EGResponse<T>> RunAsync<T>(Func<Task<EGResponse<T>>> taskFunc, Action onStart, Action onFinish)
        {
            try
            {
                onStart.Invoke();

                await Task.Delay(2000);

                var result = await taskFunc();

                return result;
            }
            catch (Exception ex)
            {
                return new EGResponse<T>
                {
                    Success = false,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                };
            }
            finally
            {
                onFinish.Invoke();
            }
        }


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
            catch (Exception ex)
            {
                setError?.Invoke(new AppException("Error", 500, new List<string>() { ex.Message}));
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
            catch (Exception ex)
            {
                setError?.Invoke(new AppException("Error", 500, new List<string>() { ex.Message }));
                return default;
            }
            finally
            {
                stopLoading?.Invoke();               
            }
        }
    }

}
