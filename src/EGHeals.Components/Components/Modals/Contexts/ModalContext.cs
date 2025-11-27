
namespace EGHeals.Components.Components.Shared.Modals.Contexts
{
    public class ModalContext
    {
        public Action? StartLoading { get; set; }
        public Action? StopLoading { get; set; }
        public Action<List<string>>? SetError { get; set; }
        public Action<string>? SetSuccess { get; set; }
        public Action<object>? Close { get; set; }
    }
}
