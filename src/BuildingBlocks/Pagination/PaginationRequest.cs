namespace BuildingBlocks.Pagination
{
    public record PaginationRequest(int PageIndex = 0, int PageSize = 50, string? FilterQuery = null, string? FilterValue = null);
}
