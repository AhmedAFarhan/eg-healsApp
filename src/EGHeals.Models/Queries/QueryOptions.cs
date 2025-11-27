namespace EGHeals.Models.Queries
{
    public class QueryOptions
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string SortBy { get; set; } = string.Empty;
        public bool SortDescending { get; set; } = false;
        public QueryFilters QueryFilters { get; set; } = new();

    }
}
