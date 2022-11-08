using Measurements.Api.Domain.Exceptions;
using Measurements.Api.Domain.Interfaces.Persistence;
using MediatR;

namespace Measurements.Api.Application.Sensors.Commands;

public class DeleteSensorCommand : IRequest
{
    public string Id { get; }

    public DeleteSensorCommand(string id)
    {
        Id = id;
    }
}

public class DeleteSensorCommandHandler : AsyncRequestHandler<DeleteSensorCommand>
{
    private readonly ILogger<DeleteSensorCommandHandler> _logger;
    private readonly ISensorRepository _repo;

    public DeleteSensorCommandHandler(ILogger<DeleteSensorCommandHandler> logger, ISensorRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    protected override async Task Handle(DeleteSensorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteSensorCommand...");

        var existing = await _repo.GetItemAsync(request.Id, cancellationToken);

        if (existing == null)
        {
            throw new EntityNotFoundException($"Sensor {request.Id} not found");
        }

        await _repo.DeleteItemAsync(request.Id, cancellationToken);
    }
}
