using AutoMapper;
using Measurements.Api.Application.Measurements.Queries;
using Measurements.Api.Domain.Interfaces.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenApi.Measurements.Api;

namespace Measurements.Api.Controllers;

public class MeasurementsController : IMeasurementsController
{
    private readonly IMeasurementRepository _repo;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public MeasurementsController(IMeasurementRepository repo, IMapper mapper, IMediator mediator)
    {
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ActionResult<MeasurementsDataResponse>> MeasurementsAsync(MeasurementFilter filter, CancellationToken cancellationToken = default)
    {
        var query = _mapper.Map<SearchMeasurementsQuery>(filter);

        return await _mediator.Send(query, cancellationToken);
    }

    public Task<IActionResult> BatchInsertAsync(IEnumerable<Measurement> body, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
