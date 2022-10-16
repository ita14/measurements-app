using Measurements.Api.Domain.Entities;
using Measurements.Api.Domain.Interfaces.Persistence;
using Measurements.Api.Infrastructure.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace Measurements.Api.Infrastructure.CosmosDbData.Repositories;

public class MeasurementsRepository : CosmosDbRepository<Measurement>, IMeasurementRepository
{
    private const char Separator = '@';

    public MeasurementsRepository(ICosmosDbContainerFactory factory, ILogger<MeasurementsRepository> logger)
        : base(factory, logger, "Measurement")
    {
    }

    public override string GenerateId(Measurement item) => $"{item.Source}{Separator}{Guid.NewGuid()}";

    public override PartitionKey ResolvePartitionKey(string entityId) => new(entityId.Split(Separator)[0]);
}
