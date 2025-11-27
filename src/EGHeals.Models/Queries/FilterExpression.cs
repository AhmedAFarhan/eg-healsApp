namespace EGHeals.Models.Queries
{
    public class FilterExpression
    {
        public string PropertyName { get; set; } = default!;
        public FilterOperator Operator { get; set; }
        public object Value { get; set; } = default!;
    }
}
