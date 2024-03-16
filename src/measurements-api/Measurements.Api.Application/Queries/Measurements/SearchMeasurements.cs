using AutoMapper;
using FluentValidation;
using Measurements.Api.Domain.Entities;
using Measurements.Api.Domain.Interfaces;
using Measurements.Api.Domain.Specifications;
using MediatR;

namespace Measurements.Api.Application.Queries.Measurements;

public class SearchMeasurementsQuery : IRequest<MeasurementsDataResponse>
{
#nullable disable

    public DateTime? StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public string Source { get; init; }
    public string OrderBy { get; init; }
    public int Limit { get; init; }
    public int Offset { get; init; }

    /// <summary>
    /// Get parsed sort condition.
    /// </summary>
    public SortCondition<Measurement> SortCondition =>
        SortCondition<Measurement>.TryParse(OrderBy, out var condition)
            ? condition
            : null;
}

public class MeasurementsDataResponse
{
    public int Count { get; set; }
    public int Total { get; set; }
    public List<Measurement> Items { get; set; }
}

public class SearchMeasurementsQueryHandler : IRequestHandler<SearchMeasurementsQuery, MeasurementsDataResponse>
{
    private readonly IReadRepository<Measurement> _repo;
    private readonly IMapper _mapper;

    public SearchMeasurementsQueryHandler(IReadRepository<Measurement> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<MeasurementsDataResponse> Handle(SearchMeasurementsQuery request, CancellationToken cancellationToken)
    {
        var searchSpec = new MeasurementSearchSpec(
            request.StartTime,
            request.EndTime,
            request.Source,
            request.Limit,
            request.Offset,
            request.SortCondition);

        var result = await _repo.ListAsync(searchSpec, cancellationToken);

        var total = await _repo.CountAsync(searchSpec, cancellationToken);

        return new MeasurementsDataResponse
        {
            Count = result.Count,
            Total = total,
            Items = _mapper.Map<List<Measurement>>(result)
        };
    }
}

public class SearchMeasurementsQueryValidator : AbstractValidator<SearchMeasurementsQuery>
{
    public SearchMeasurementsQueryValidator()
    {
        RuleFor(x => x.OrderBy)
            .Must(BeValidOrderByFilter)
            .When(x => !string.IsNullOrWhiteSpace(x.OrderBy))
            .WithMessage(
                "Order by condition format must be [column name]:[asc|desc] and column must exist. Example time:asc");

        RuleFor(x => x.Limit).InclusiveBetween(1, 1000);
        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .When(x => x.EndTime.HasValue && x.StartTime.HasValue);
    }

    private static bool BeValidOrderByFilter(string orderBy)
    {
        return SortCondition<Measurement>.TryParse(orderBy, out _);
    }
}
