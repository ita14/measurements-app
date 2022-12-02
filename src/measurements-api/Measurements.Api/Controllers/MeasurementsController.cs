using AutoMapper;
using Measurements.Api.Application.Measurements.Commands;
using Measurements.Api.Application.Measurements.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenApi.Measurements.Api;

namespace Measurements.Api.Controllers;

public class MeasurementsController : ControllerBase, IMeasurementsController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public MeasurementsController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ActionResult<MeasurementsDataResponse>> MeasurementsAsync(DateTime? startTime, DateTime? endTime, string source, string orderBy, int limit, int offset,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var query = new SearchMeasurementsQuery
        {
            StartTime = startTime,
            EndTime = endTime,
            Source = source,
            OrderBy = orderBy,
            Limit = limit,
            Offset = offset
        };

        return await _mediator.Send(query, cancellationToken);
    }

    public async Task<IActionResult> BatchInsertAsync(IEnumerable<Measurement> body, CancellationToken cancellationToken = default)
    {
        var command = new InsertMeasurementsCommand(body);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}
