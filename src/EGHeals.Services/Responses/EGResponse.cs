using EGHeals.Services.Responses.Abstractions;

namespace EGHeals.Services.Responses
{
    public class EGResponse<T> : EGResponseBase
    {
        public T? Data { get; set; }
    }
}
