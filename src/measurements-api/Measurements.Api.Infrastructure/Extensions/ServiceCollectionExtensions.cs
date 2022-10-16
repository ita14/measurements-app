using Measurements.Api.Infrastructure.AppSettings;
using Measurements.Api.Infrastructure.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.DependencyInjection;

namespace Measurements.Api.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCosmosDb(this IServiceCollection services, CosmosDbSettings settings)
    {
        services.AddSingleton<ICosmosDbContainerFactory>(_ =>
        {
            CosmosClient client = new CosmosClientBuilder(settings.ConnectionString)
                .WithSerializerOptions(new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                })
                .WithBulkExecution(true)
                .WithThrottlingRetryOptions(TimeSpan.FromSeconds(30), 10)
                .Build();

            return new CosmosDbContainerFactory(
                client,
                settings.DatabaseName,
                settings.Containers);
        });

        return services;
    }
}
