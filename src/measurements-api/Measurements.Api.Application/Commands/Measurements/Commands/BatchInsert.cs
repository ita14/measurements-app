using AutoMapper;
using Measurements.Api.Domain.Entities;
using Measurements.Api.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Measurements.Api.Application.Commands.Measurements.Commands;

public class InsertMeasurementsCommand : IRequest
{
    public InsertMeasurementsCommand(IEnumerable<Measurement> items) => Items = items;

    public IEnumerable<Measurement> Items { get; }
}

public class InsertMeasurementsCommandHandler : IRequestHandler<InsertMeasurementsCommand>
{
    private readonly ILogger<InsertMeasurementsCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Measurement> _repo;

    public InsertMeasurementsCommandHandler(ILogger<InsertMeasurementsCommandHandler> logger,
        IRepository<Measurement> repo, IMapper mapper)
    {
        _logger = logger;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task Handle(InsertMeasurementsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling InsertMeasurementsCommand...");

        foreach (Measurement measurement in request.Items.Where(x => string.IsNullOrWhiteSpace(x.Id)))
        {
            measurement.Id = Guid.NewGuid().ToString();
        }

        await _repo.AddRangeAsync(_mapper.Map<IEnumerable<Measurement>>(request.Items),
             cancellationToken);
    }
}
