using AutoMapper;
using Measurements.Api.Application.Sensors.Commands;
using Measurements.Api.Domain.Interfaces.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenApi.Measurements.Api;

namespace Measurements.Api.Controllers;

public class SensorsController : SensorsControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ISensorRepository _repo;

    public SensorsController(IMapper mapper, IMediator mediator, ISensorRepository repo)
    {
        _mapper = mapper;
        _mediator = mediator;
        _repo = repo;
    }

    [AllowAnonymous]
    public override async Task<ActionResult<ICollection<Sensor>>> SensorsGet(CancellationToken cancellationToken = default(CancellationToken))
    {
        var result = await _repo.SearchItemsAsync(null, cancellationToken);

        return Ok(_mapper.Map<IEnumerable<Sensor>>(result));
    }

    public override async Task<ActionResult<Sensor>> SensorsPost(Sensor body, CancellationToken cancellationToken = default(CancellationToken))
    {
        var command = _mapper.Map<CreateSensorCommand>(body);

        return await _mediator.Send(command, cancellationToken);
    }

    [AllowAnonymous]
    public override async Task<ActionResult<Sensor>> SensorsGet(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
        var result = await _repo.GetItemAsync(id, cancellationToken);

        if (result != null)
        {
            return Ok(_mapper.Map<Sensor>(result));
        }

        return NotFound();
    }

    public override async Task<IActionResult> SensorsPut(Sensor body, string id, CancellationToken cancellationToken = default(CancellationToken))
    {
        var command = _mapper.Map<UpdateSensorCommand>(body);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    public override async Task<IActionResult> SensorsDelete(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
        var command = new DeleteSensorCommand(id);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}
