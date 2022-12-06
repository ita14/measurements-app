using AutoMapper;
using FluentValidation;
using Measurements.Api.Domain.Exceptions;
using Measurements.Api.Domain.Interfaces.Persistence;
using MediatR;
using OpenApi.Measurements.Api;

namespace Measurements.Api.Application.Sensors.Commands;

public class UpdateSensorCommand : Sensor, IRequest
{
}

public class UpdateSensorCommandHandler : AsyncRequestHandler<UpdateSensorCommand>
{
    private readonly ILogger<UpdateSensorCommandHandler> _logger;
    private readonly ISensorRepository _repo;
    private readonly IMapper _mapper;

    public UpdateSensorCommandHandler(ILogger<UpdateSensorCommandHandler> logger,
        ISensorRepository repo, IMapper mapper)
    {
        _logger = logger;
        _repo = repo;
        _mapper = mapper;
    }

    protected override async Task Handle(UpdateSensorCommand request, CancellationToken ct)
    {
        _logger.LogInformation("Handling UpdateSensorCommandHandler...");

        var existing = await _repo.GetItemAsync(request.Id, ct);

        if (existing is null)
        {
            throw new EntityNotFoundException($"Sensor {request.Id} not found");
        }

        await _repo.UpdateItemAsync(request.Id, _mapper.Map<Domain.Entities.Sensor>(request), ct);
    }
}

public class UpdateSensorCommandValidator : AbstractValidator<UpdateSensorCommand>
{
    public UpdateSensorCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
