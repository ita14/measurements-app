using AutoMapper;
using FluentValidation;
using Measurements.Api.Domain.Entities;
using Measurements.Api.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Measurements.Api.Application.Commands.Sensors;

public class UpdateSensorCommand : Sensor, IRequest
{
}

public class UpdateSensorCommandHandler : IRequestHandler<UpdateSensorCommand>
{
    private readonly ILogger<UpdateSensorCommandHandler> _logger;
    private readonly IRepository<Sensor> _repo;
    private readonly IMapper _mapper;

    public UpdateSensorCommandHandler(ILogger<UpdateSensorCommandHandler> logger,
        IRepository<Sensor> repo, IMapper mapper)
    {
        _logger = logger;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task Handle(UpdateSensorCommand request, CancellationToken ct)
    {
        _logger.LogInformation("Handling UpdateSensorCommandHandler...");

        // TODO: handle not found

        await _repo.UpdateAsync(_mapper.Map<Sensor>(request), ct);
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
