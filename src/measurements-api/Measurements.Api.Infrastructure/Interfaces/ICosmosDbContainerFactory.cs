namespace Measurements.Api.Infrastructure.Interfaces
{
    public interface ICosmosDbContainerFactory
    {
        ICosmosDbContainer GetContainer(string containerName);
        Task EnsureDbSetupAsync();
    }
}
