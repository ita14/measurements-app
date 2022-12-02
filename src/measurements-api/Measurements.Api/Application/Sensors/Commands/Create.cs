using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Measurements.Api.Domain.Interfaces.Persistence;
using MediatR;
using OpenApi.Measurements.Api;

namespace Measurements.Api.Application.Sensors.Commands;

public class CreateSensorCommand : Sensor, IRequest<Sensor>
{
}

public class CreateSensorCommandHandler : IRequestHandler<CreateSensorCommand, Sensor>
{
    private readonly ILogger<CreateSensorCommandHandler> _logger;
    private readonly ISensorRepository _repo;
    private readonly IMapper _mapper;

    public CreateSensorCommandHandler(ILogger<CreateSensorCommandHandler> logger,
        ISensorRepository repo, IMapper mapper)
    {
        _logger = logger;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Sensor> Handle(CreateSensorCommand request, CancellationToken ct)
    {
        _logger.LogInformation("Handling CreateSensorCommand...");

        // TODO: Change the sensor identifier to id or validate identifier duplicates

        var item = _mapper.Map<Domain.Entities.Sensor>(request);

        return _mapper.Map<Sensor>(await _repo.AddItemAsync(item, ct));
    }
}
