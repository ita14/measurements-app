using Measurements.Api.Infrastructure.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Measurements.Api.Infrastructure
{
    public class CosmosDbContainer : ICosmosDbContainer
    {
        public Container Container { get; }

        public CosmosDbContainer(CosmosClient cosmosClient,
            string databaseName, string containerName)
        {
            Container = cosmosClient.GetContainer(databaseName, containerName);
        }
    }
}
