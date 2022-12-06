using Measurements.Api.Domain.Entities;
using Measurements.Api.Domain.Interfaces.Persistence;
using Measurements.Api.Infrastructure.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace Measurements.Api.Infrastructure.CosmosDbData.Repositories;

public class SensorRepository : CosmosDbRepository<Sensor>, ISensorRepository
{
    public SensorRepository(ICosmosDbContainerFactory factory, ILogger<SensorRepository> logger)
        : base(factory, logger, "Sensor")
    {
    }

    public override string GenerateId(Sensor entity) => entity.Id;

    public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId);
}
