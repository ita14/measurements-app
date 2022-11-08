using AutoMapper;
using Measurements.Api.Domain.Interfaces.Persistence;
using MediatR;
using OpenApi.Measurements.Api;

namespace Measurements.Api.Application.Measurements.Commands;

public class InsertMeasurementsCommand : IRequest
{
    public IEnumerable<Measurement> Items { get; }

    public InsertMeasurementsCommand(IEnumerable<Measurement> items)
    {
        Items = items;
    }
}

public class InsertMeasurementsCommandHandler : AsyncRequestHandler<InsertMeasurementsCommand>
{
    private readonly ILogger<InsertMeasurementsCommandHandler> _logger;
    private readonly IMeasurementRepository _repo;
    private readonly IMapper _mapper;

    public InsertMeasurementsCommandHandler(ILogger<InsertMeasurementsCommandHandler> logger,
        IMeasurementRepository repo, IMapper mapper)
    {
        _logger = logger;
        _repo = repo;
        _mapper = mapper;
    }

    protected override async Task Handle(InsertMeasurementsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling InsertMeasurementsCommand...");

        await _repo.BatchInsertAsync(_mapper.Map<IEnumerable<Domain.Entities.Measurement>>(request.Items), cancellationToken);
    }
}
