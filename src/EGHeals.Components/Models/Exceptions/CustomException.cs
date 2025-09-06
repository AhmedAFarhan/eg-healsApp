namespace EGHeals.Components.Models.Exceptions
{
    public class CustomException : Exception
    {
        public string Title { get; }

        public CustomException(string title, string description) : base(description)
        {
            Title = title;
        }
    }
}
