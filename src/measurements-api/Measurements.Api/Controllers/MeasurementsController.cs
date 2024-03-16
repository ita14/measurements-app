using AutoMapper;
using Measurements.Api.Application.Commands.Measurements.Commands;
using Measurements.Api.Application.Queries.Measurements;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenApi.Measurements.Api;
using MeasurementsDataResponse = OpenApi.Measurements.Api.MeasurementsDataResponse;

namespace Measurements.Api.Controllers;

public class MeasurementsController : MeasurementsControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public MeasurementsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    public override async Task<ActionResult<MeasurementsDataResponse>> Measurements(DateTime? startTime, DateTime? endTime, string source, string orderBy = "time:asc", int? limit = 100,
        int? offset = 0, CancellationToken cancellationToken = default)
    {
        var query = new SearchMeasurementsQuery
        {
            StartTime = startTime,
            EndTime = endTime,
            Source = source,
            OrderBy = orderBy,
            Limit = limit ?? 100,
            Offset = offset ?? 0
        };

        var result = await _mediator.Send(query, cancellationToken);

        return _mapper.Map<MeasurementsDataResponse>(result);
    }

    public override async Task<IActionResult> BatchInsert(IEnumerable<Measurement> body, CancellationToken cancellationToken = default)
    {
        var command = new InsertMeasurementsCommand(_mapper.Map<List<Domain.Entities.Measurement>>(body));

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}
