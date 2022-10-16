using Microsoft.Azure.Cosmos;

namespace Measurements.Api.Infrastructure.Interfaces
{
    public interface ICosmosDbContainer
    {
        Container Container { get; }
    }
}
