using AutoMapper;
using FluentValidation;
using Measurements.Api.Domain.Interfaces.Persistence;
using Measurements.Api.Domain.Specifications;
using MediatR;
using OpenApi.Measurements.Api;

namespace Measurements.Api.Application.Measurements.Queries;

public class SearchMeasurementsQuery: IRequest<MeasurementsDataResponse>
{
    #nullable disable

    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Source { get; set; }
    public string OrderBy { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }

    /// <summary>
    /// Get parsed sort condition.
    /// </summary>
    public SortCondition<Domain.Entities.Measurement> SortCondition =>
        SortCondition<Domain.Entities.Measurement>.TryParse(OrderBy, out var condition)
            ? condition
            : null;
}

public class SearchMeasurementsQueryHandler : IRequestHandler<SearchMeasurementsQuery, MeasurementsDataResponse>
{
    private readonly IMeasurementRepository _repo;
    private readonly IMapper _mapper;

    public SearchMeasurementsQueryHandler(IMeasurementRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<MeasurementsDataResponse> Handle(SearchMeasurementsQuery request, CancellationToken cancellationToken)
    {
        var spec = new MeasurementSearchSpec(
            request.StartTime,
            request.EndTime,
            request.Source,
            request.Limit,
            request.Offset,
            request.SortCondition);

        var result = await _repo.SearchItemsAsync(spec, cancellationToken);

        return new MeasurementsDataResponse
        {
            Count = result.Count(),
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
    }

    private static bool BeValidOrderByFilter(string orderBy)
    {
        return SortCondition<Measurement>.TryParse(orderBy, out _);
    }
}
