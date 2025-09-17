namespace BuildingBlocks.Responses
{
    public class EGResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; } = default!;
        public T Data { get; set; } = default!;
        public List<string> Errors { get; set; } = default!;
    }
}
