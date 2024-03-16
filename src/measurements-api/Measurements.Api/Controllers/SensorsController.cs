using AutoMapper;
using Measurements.Api.Application.Commands.Sensors;
using Measurements.Api.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenApi.Measurements.Api;

namespace Measurements.Api.Controllers;

public class SensorsController : SensorsControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IReadRepository<Domain.Entities.Sensor> _repo;

    public SensorsController(IMapper mapper, IMediator mediator, IReadRepository<Domain.Entities.Sensor> repo)
    {
        _mapper = mapper;
        _mediator = mediator;
        _repo = repo;
    }

    [AllowAnonymous]
    public override async Task<ActionResult<ICollection<Sensor>>> SensorsGet(
        CancellationToken cancellationToken = default)
    {
        IEnumerable<Domain.Entities.Sensor> items = await _repo.ListAsync(cancellationToken);

        return Ok(_mapper.Map<IEnumerable<Sensor>>(items));
    }

    public override async Task<ActionResult<Sensor>> SensorsPost(Sensor body,
        CancellationToken cancellationToken = default)
    {
        CreateSensorCommand? command = _mapper.Map<CreateSensorCommand>(body);

        var result = await _mediator.Send(command, cancellationToken);

        return _mapper.Map<Sensor>(result);
    }

    [AllowAnonymous]
    public override async Task<ActionResult<Sensor>> SensorsGet(string id,
        CancellationToken cancellationToken = default)
    {
        Domain.Entities.Sensor? result = await _repo.GetByIdAsync(id, cancellationToken);

        if (result != null)
        {
            return Ok(_mapper.Map<Sensor>(result));
        }

        return NotFound();
    }

    public override async Task<IActionResult> SensorsPut(Sensor body, string id,
        CancellationToken cancellationToken = default)
    {
        body.Id = id;
        UpdateSensorCommand? command = _mapper.Map<UpdateSensorCommand>(body);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    public override async Task<IActionResult> SensorsDelete(string id, CancellationToken cancellationToken = default)
    {
        DeleteSensorCommand command = new(id);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}
