using Measurements.Api.Application.Measurements.Commands;
using Measurements.Api.Application.Measurements.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenApi.Measurements.Api;

namespace Measurements.Api.Controllers;

public class MeasurementsController : MeasurementsControllerBase
{
    private readonly IMediator _mediator;

    public MeasurementsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    public override async Task<ActionResult<MeasurementsDataResponse>> Measurements(DateTime? startTime, DateTime? endTime, string source, string orderBy = "time:asc", int? limit = 100,
        int? offset = 0, CancellationToken cancellationToken = default(CancellationToken))
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

        return await _mediator.Send(query, cancellationToken);
    }

    public override async Task<IActionResult> BatchInsert(IEnumerable<Measurement> body, CancellationToken cancellationToken = default(CancellationToken))
    {
        var command = new InsertMeasurementsCommand(body);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}
