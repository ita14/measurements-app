using Measurements.Api.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace Measurements.Api.Infrastructure.CosmosDbData.Interfaces;

public interface IContainerContext<in T> where T : BaseEntity
{
    string ContainerName { get; }
    string GenerateId(T entity);
    PartitionKey ResolvePartitionKey(string entityId);
}
