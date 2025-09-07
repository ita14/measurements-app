using Measurements.Api.Infrastructure.AppSettings;
using Measurements.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Measurements.Api.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgreSql(this IServiceCollection services, PostgreSqlSettings settings)
    {
        services.AddDbContext<MeasurementsDbContext>(options => 
            options.UseNpgsql(settings.ConnectionString));

        return services;
    }
}
