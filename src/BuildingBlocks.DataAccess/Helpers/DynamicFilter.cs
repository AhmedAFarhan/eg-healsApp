using System.Linq.Expressions;

namespace BuildingBlocks.DataAccess.Helpers
{
    public static class DynamicFilter
    {
        public static Expression<Func<T, bool>>? BuildDynamicFilter<T>(string? searchValue, params string[] propertyNames)
        {
            if (string.IsNullOrWhiteSpace(searchValue))
                return null;

            var parameter = Expression.Parameter(typeof(T), "e");
            Expression? combinedExpression = null;

            string[] excludes = { "Id", "CreatedAt", "CreatedBy", "LastModifiedAt", "LastModifiedBy" };

            // If "all" is passed, get all properties dynamically
            if (propertyNames.Length == 1 && propertyNames[0].Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                propertyNames = typeof(T)
                    .GetProperties()
                    .Where(p => (p.PropertyType == typeof(string) || p.PropertyType.IsValueType) && (!excludes.Contains(p.Name))) // Get string & value types (int, double, DateTime, etc.)
                    .Select(p => p.Name)
                    .ToArray();
            }

            foreach (var propertyName in propertyNames)
            {
                var property = Expression.Property(parameter, propertyName);
                var propertyType = property.Type;

                try
                {
                    Expression filterExpression;

                    if (propertyType == typeof(string))
                    {
                        // Apply "Contains" for strings
                        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var constant = Expression.Constant(searchValue);
                        filterExpression = Expression.Call(property, containsMethod!, constant);
                    }
                    else
                    {
                        // Convert searchValue to match property type
                        var convertedValue = Convert.ChangeType(searchValue, propertyType);
                        var constant = Expression.Constant(convertedValue, propertyType);

                        // Apply equality check for numbers, DateTime, and other types
                        filterExpression = Expression.Equal(property, constant);
                    }

                    combinedExpression = combinedExpression == null
                        ? filterExpression
                        : Expression.OrElse(combinedExpression, filterExpression);
                }
                catch
                {
                    // Ignore properties that cannot be converted
                    continue;
                }
            }

            if (combinedExpression == null)
                throw new ArgumentException("No valid properties for filtering.");

            return Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
        }
    }
}
