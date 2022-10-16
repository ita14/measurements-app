using System.Linq.Expressions;

namespace Measurements.Api.Domain.Specifications;

public static class ExpressionUtils
{
    /// <summary>
    /// Build expression for property access i.e. x => x.Time
    /// </summary>
    /// <param name="propertyName"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static Expression<Func<TSource, object?>> GetPropertyAccessExpression<TSource>(string propertyName)
    {
        var param = Expression.Parameter(typeof(TSource), "x");
        Expression conversion = Expression.Convert(Expression.Property(param, propertyName), typeof(object));
        return Expression.Lambda<Func<TSource, object?>>(conversion, param);
    }
}
