namespace EGHeals.Models.Queries
{
    public class QueryFilters
    {
        public List<FilterExpression> Filters { get; set; } = new();
        public bool UseOrLogic { get; set; } = false;
    }
}
