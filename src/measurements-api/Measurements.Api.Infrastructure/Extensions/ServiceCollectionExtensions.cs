using Measurements.Api.Infrastructure.AppSettings;
using Measurements.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Measurements.Api.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCosmosDb(this IServiceCollection services, CosmosDbSettings settings)
    {
        services.AddDbContext<MeasurementsDbContext>(options => options.UseCosmos(
            settings.ConnectionString,
            settings.DatabaseName));

        return services;
    }
}
