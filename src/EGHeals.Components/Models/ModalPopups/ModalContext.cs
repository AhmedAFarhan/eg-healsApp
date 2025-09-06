using EGHeals.Components.Models.Exceptions;

namespace EGHeals.Components.Models.ModalPopups
{
    public class ModalContext
    {
        public Action? StartLoading { get; set; }
        public Action? StopLoading { get; set; }
        public Action<CustomException>? SetError { get; set; }
        public Action<string>? SetSuccess { get; set; }
        public Action? Close { get; set; }
    }
}
