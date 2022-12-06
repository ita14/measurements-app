using Ardalis.Specification;
using Measurements.Api.Domain.Entities;

namespace Measurements.Api.Domain.Specifications;

public sealed class PagingSpec<T> : Specification<T> where T : BaseEntity
{
    public PagingSpec(int limit, int offset, SortCondition<T>? sortCondition)
    {
        Query.Skip(offset);
        Query.Take(limit);

        if (sortCondition is not null)
        {
            Query.OrderBy(sortCondition.PropertyExpression,           sortCondition.Direction == SortDirection.Ascending);
            Query.OrderByDescending(sortCondition.PropertyExpression, sortCondition.Direction == SortDirection.Descending);
        }
    }
}
