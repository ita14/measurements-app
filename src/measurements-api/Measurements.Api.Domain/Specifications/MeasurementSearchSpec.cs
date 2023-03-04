using Ardalis.Specification;
using Measurements.Api.Domain.Entities;

namespace Measurements.Api.Domain.Specifications;

public sealed class MeasurementSearchSpec : Specification<Measurement>
{
    public MeasurementSearchSpec(DateTime? startTime, DateTime? endTime, string source,
        int limit, int offset, SortCondition<Measurement>? sortCondition)
    {
        Query.Where(x => x.Time >= startTime, startTime is not null);
        Query.Where(x => x.Time < endTime, endTime is not null);
        Query.Where(x => x.Source == source, !string.IsNullOrWhiteSpace(source));
        Query.Skip(offset);
        Query.Take(limit);

        if (sortCondition is not null)
        {
            Query.OrderBy(sortCondition.PropertyExpression, sortCondition.Direction == SortDirection.Ascending);
            Query.OrderByDescending(sortCondition.PropertyExpression, sortCondition.Direction == SortDirection.Descending);
        }
    }
}
