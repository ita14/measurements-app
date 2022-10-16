using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace Measurements.Api.Domain.Specifications;

public enum SortDirection
{
    Ascending = 0,
    Descending = 1
}

public class SortCondition<T> where T : class
{
    public SortDirection Direction { get; }
    public Expression<Func<T, object?>> PropertyExpression { get; }

    private SortCondition(SortDirection direction, Expression<Func<T, object?>> expression)
    {
        Direction = direction;
        PropertyExpression = expression;
    }

    public static bool TryParse(string orderBy, [NotNullWhen(true)] out SortCondition<T>? condition)
    {
        condition = null;

        var split = orderBy.Split(':');

        if (split.Length != 2)
        {
            return false;
        }

        var column = split[0];
        var dir = split[1];

        SortDirection? direction = dir.ToLower() switch
        {
            "asc" => SortDirection.Ascending,
            "desc" => SortDirection.Descending,
            _ => null
        };

        if (direction is null)
        {
            return false;
        }

        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
        if (typeof(T).GetProperty(column, bindingFlags) == null)
        {
            return false;
        }

        condition = new SortCondition<T>(direction.Value, ExpressionUtils.GetPropertyAccessExpression<T>(column));

        return true;
    }
}
