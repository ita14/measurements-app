﻿using AutoMapper;
using FluentValidation;
using Measurements.Api.Domain.Interfaces.Persistence;
using MediatR;
using OpenApi.Measurements.Api;
using ValidationException = Measurements.Api.Domain.Exceptions.ValidationException;

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

        var existing = await _repo.GetItemAsync(request.Id, ct);

        if (existing is not null)
        {
            throw new ValidationException($"Sensor {request.Id} already exists.");
        }

        var item = _mapper.Map<Domain.Entities.Sensor>(request);

        return _mapper.Map<Sensor>(await _repo.AddItemAsync(item, ct));
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
