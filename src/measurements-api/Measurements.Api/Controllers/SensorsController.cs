using AutoMapper;
using Measurements.Api.Application.Sensors.Commands;
using Measurements.Api.Domain.Interfaces.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenApi.Measurements.Api;

namespace Measurements.Api.Controllers;

public class SensorsController : ControllerBase, ISensorsController
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

    public async Task<ActionResult<ICollection<Sensor>>> SensorsGetAsync(CancellationToken cancellationToken = default)
    {
        var result = await _repo.SearchItemsAsync(null, cancellationToken);

        return Ok(_mapper.Map<IEnumerable<Sensor>>(result));
    }

    public async Task<ActionResult<Sensor>> SensorsPostAsync(Sensor body, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateSensorCommand>(body);

        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<ActionResult<Sensor>> SensorsGetAsync(string id, CancellationToken cancellationToken = default)
    {
        var result = await _repo.GetItemAsync(id, cancellationToken);

        if (result != null)
        {
            return Ok(_mapper.Map<Sensor>(result));
        }

        return NotFound();
    }

    public async Task<ActionResult<Sensor>> SensorsPutAsync(Sensor body, string id, CancellationToken cancellationToken = default(CancellationToken))
    {
        var command = _mapper.Map<UpdateSensorCommand>(body);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    public async Task<IActionResult> SensorsDeleteAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
        var command = new DeleteSensorCommand(id);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}
