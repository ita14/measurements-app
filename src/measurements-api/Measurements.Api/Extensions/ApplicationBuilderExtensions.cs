using Bogus;
using Measurements.Api.Domain.Entities;
using Measurements.Api.Domain.Interfaces.Persistence;
using Measurements.Api.Infrastructure.Interfaces;
using Sensor = Measurements.Api.Domain.Entities.Sensor;

namespace Measurements.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task EnsureCosmosDbIsCreated(this IApplicationBuilder builder)
    {
        using IServiceScope serviceScope = builder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        var factory = serviceScope.ServiceProvider.GetRequiredService<ICosmosDbContainerFactory>();

        await factory.EnsureDbSetupAsync();
    }

    public static async Task SeedTestDataIfEmptyAsync(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var sensorRepo = scope.ServiceProvider.GetRequiredService<ISensorRepository>();
        var measurementsRepo = scope.ServiceProvider.GetRequiredService<IMeasurementRepository>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        if (await sensorRepo.GetCountAsync() > 0)
        {
            return;
        }

        var sensors = configuration.GetSection("Sensors").Get<List<Sensor>>();

        if (sensors is null || sensors.Count == 0)
        {
            return;
        }

        await sensorRepo.BatchInsertAsync(sensors);

        const int measurementCount = 1000;
        var interval = TimeSpan.FromMinutes(10);
        var now = DateTime.UtcNow;

        foreach (var sensor in sensors)
        {
            var testAcceleration = new Faker<Acceleration>()
                .RuleForType(typeof(double), f => f.Random.Double(1000, 10005));

            var measurements = new Faker<Measurement>()
                .RuleFor(o => o.Time, f => now.Subtract(interval * f.IndexVariable++))
                .RuleFor(o => o.Source, f => sensor.Id)
                .RuleFor(o => o.Temperature, f => f.Random.Double(15, 25))
                .RuleFor(o => o.Pressure, f => f.Random.Double(1000, 1010))
                .RuleFor(o => o.Humidity, f => f.Random.Double(20, 60))
                .RuleFor(o => o.Battery, f => f.Random.Double(2500, 3000))
                .RuleFor(o => o.Acceleration, testAcceleration.Generate())
                .Generate(measurementCount);

            await measurementsRepo.BatchInsertAsync(measurements);
        }
    }
}
