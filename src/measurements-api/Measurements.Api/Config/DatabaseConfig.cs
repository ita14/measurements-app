using Measurements.Api.Domain.Interfaces.Persistence;
using Measurements.Api.Infrastructure.AppSettings;
using Measurements.Api.Infrastructure.CosmosDbData.Repositories;
using Measurements.Api.Infrastructure.Extensions;

namespace Measurements.Api.Config;

public static class DatabaseConfig
{
    public static void SetupCosmosDb(this IServiceCollection services, IConfiguration configuration)
    {
        CosmosDbSettings cosmosDbConfig = configuration.GetSection("CosmosDB").Get<CosmosDbSettings>();

        services.AddCosmosDb(cosmosDbConfig);
        services.AddScoped<IMeasurementRepository, MeasurementsRepository>();
        services.AddScoped<ISensorRepository, SensorRepository>();
    }
}
