using AutoMapper;
using FluentValidation;
using Measurements.Api.Domain.Entities;
using Measurements.Api.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = Measurements.Api.Domain.Exceptions.ValidationException;

namespace Measurements.Api.Application.Commands.Sensors;

public class CreateSensorCommand : Sensor, IRequest<Sensor>
{
}

public class CreateSensorCommandHandler : IRequestHandler<CreateSensorCommand, Sensor>
{
    private readonly ILogger<CreateSensorCommandHandler> _logger;
    private readonly IRepository<Sensor> _repo;
    private readonly IMapper _mapper;

    public CreateSensorCommandHandler(ILogger<CreateSensorCommandHandler> logger,
        IRepository<Sensor> repo, IMapper mapper)
    {
        _logger = logger;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Sensor> Handle(CreateSensorCommand request, CancellationToken ct)
    {
        _logger.LogInformation("Handling CreateSensorCommand...");

        var existing = await _repo.GetByIdAsync(request.Id, ct);

        if (existing is not null)
        {
            throw new ValidationException($"Sensor {request.Id} already exists.");
        }

        var item = _mapper.Map<Sensor>(request);

        return _mapper.Map<Sensor>(await _repo.AddAsync(item, ct));
    }
}

public class CreateSensorCommandValidator : AbstractValidator<CreateSensorCommand>
{
    public CreateSensorCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
