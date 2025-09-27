using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using BuildingBlocks.DataAccessAbstraction.Models;
using BuildingBlocks.DataAccessAbstraction.Enums;

namespace BuildingBlocks.DataAccess.Helpers
{
    public class AdvancedDynamicFilterBuilder
    {
        //public static Expression<Func<T, bool>>? BuildPredicate<T>(IEnumerable<FilterCriteria> filters)
        //{
        //    if (filters == null || !filters.Any())
        //        return null;

        //    var predicate = PredicateBuilder.New<T>(true);

        //    foreach (var filter in filters)
        //    {
        //        var targetProperties = filter.Property.Equals("all", StringComparison.OrdinalIgnoreCase) ? typeof(T).GetProperties().Where(p => p.PropertyType == typeof(string) ||
        //                                                                                                                                        p.PropertyType.IsValueType ||
        //                                                                                                                                        Nullable.GetUnderlyingType(p.PropertyType) != null)
        //                                                                                                 : new[] { typeof(T).GetProperty(filter.Property)! };

        //        foreach (var prop in targetProperties)
        //        {
        //            if (prop == null) continue;

        //            Expression<Func<T, bool>> expr;

        //            if (prop.PropertyType == typeof(string))
        //            {
        //                expr = filter.Operator switch
        //                {
        //                    FilterOperator.CONTAINS => e => EF.Property<string>(e, prop.Name).Contains(filter.Value),
        //                    FilterOperator.START_WITH => e => EF.Property<string>(e, prop.Name).StartsWith(filter.Value),
        //                    FilterOperator.EQUAL => e => EF.Property<string>(e, prop.Name).Equals(filter.Value),
        //                    _ => e => EF.Property<string>(e, prop.Name) == filter.Value
        //                };
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
        //                    var convertedValue = Convert.ChangeType(filter.Value, targetType);

        //                    expr = e => EF.Property<object>(e, prop.Name).Equals(convertedValue);
        //                }
        //                catch
        //                {
        //                    continue;
        //                }
        //            }

        //            // Combine based on logic
        //            if (filter.Logic == FilterLogic.AND)
        //            {
        //                predicate = predicate.And(expr);
        //            }
        //            else
        //            {
        //                predicate = predicate.Or(expr);
        //            }
        //        }
        //    }

        //    return predicate;
        //}

        //public static Expression<Func<T, bool>>? BuildPredicate<T>(IEnumerable<FilterCriteria> filters)
        //{
        //    if (filters == null || !filters.Any()) return null;

        //    var predicate = PredicateBuilder.New<T>(true); // start with "true" (neutral for AND)

        //    foreach (var filter in filters)
        //    {
        //        var propertyName = filter.Property;
        //        var value = filter.Value;
        //        var op = filter.Operator;//.ToLower();
        //        var logic = filter.Logic;

        //        var property = typeof(T).GetProperty(propertyName);

        //        if (property == null) continue;

        //        Expression<Func<T, bool>> expr;

        //        if (property.PropertyType == typeof(string))
        //        {
        //            expr = op switch
        //            {
        //                FilterOperator.CONTAINS => e => EF.Property<string>(e, propertyName).Contains(value),
        //                FilterOperator.START_WITH => e => EF.Property<string>(e, propertyName).StartsWith(value),
        //                FilterOperator.EQUAL => e => EF.Property<string>(e, propertyName).Equals(value),
        //                _ => e => EF.Property<string>(e, propertyName) == value
        //            };
        //        }
        //        else
        //        {
        //            try
        //            {
        //                var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
        //                var convertedValue = Convert.ChangeType(value, targetType);

        //                expr = e => EF.Property<object>(e, propertyName).Equals(convertedValue);
        //            }
        //            catch
        //            {
        //                continue;
        //            }
        //        }

        //        // Combine based on logic
        //        if (logic == FilterLogic.AND)
        //        {
        //            predicate = predicate.And(expr);
        //        }
        //        else
        //        {
        //            predicate = predicate.Or(expr);
        //        }
        //    }

        //    return predicate;
        //}
    }
}
