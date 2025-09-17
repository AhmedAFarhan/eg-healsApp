namespace BuildingBlocks.Exceptions
{
    public class AppException : Exception
    {
        public string Title { get; set; } = default!;
        public int StatusCode { get; set; }

        public AppException(string message) : base(message) { }

        public AppException(string title, int statusCode, string message) : base(message)
        {
            Title = title;
            StatusCode = statusCode;
        }
    }
}
