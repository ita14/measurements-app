using FluentValidation;
using Measurements.Api.Domain.Exceptions;
using Measurements.Api.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Measurements.Api.Application.Commands.Sensors;

public class DeleteSensorCommand : IRequest
{
    public string Id { get; }

    public DeleteSensorCommand(string id)
    {
        Id = id;
    }
}

public class DeleteSensorCommandHandler : IRequestHandler<DeleteSensorCommand>
{
    private readonly ILogger<DeleteSensorCommandHandler> _logger;
    private readonly IRepository<Domain.Entities.Sensor> _repo;

    public DeleteSensorCommandHandler(ILogger<DeleteSensorCommandHandler> logger, IRepository<Domain.Entities.Sensor> repo)
    {
        _logger = logger;
        _repo = repo;
    }

    public async Task Handle(DeleteSensorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteSensorCommand...");

        var existing = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (existing is null)
        {
            throw new EntityNotFoundException($"Sensor {request.Id} not found");
        }

        await _repo.DeleteAsync(existing, cancellationToken);
    }
}

public class DeleteSensorCommandValidator : AbstractValidator<DeleteSensorCommand>
{
    public DeleteSensorCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
