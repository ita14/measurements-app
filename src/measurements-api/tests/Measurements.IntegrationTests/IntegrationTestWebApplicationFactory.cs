using DotNet.Testcontainers;
using DotNet.Testcontainers.Builders;
using Measurements.Api.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;

namespace Measurements.IntegrationTests;

// ReSharper disable once ClassNeverInstantiated.Global
public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private const bool UseHostDb = false;
    private readonly PostgreSqlContainer _postgreSqlContainer;

    public IntegrationTestWebApplicationFactory()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Debug)
                .AddConsole();
        });
        var logger = loggerFactory.CreateLogger("test-containers");
        ConsoleLogger.Instance.DebugLogLevelEnabled = true;

        _postgreSqlContainer = new PostgreSqlBuilder()
            .WithImage("timescale/timescaledb:latest-pg16")
            .WithDatabase("MeasurementsDB")
            .WithUsername("measurements_user")
            .WithPassword("measurements_pass")
            .WithPortBinding(5432, true)
            .WithLogger(logger)
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<MeasurementsDbContext>));

            services.AddDbContext<MeasurementsDbContext>(options =>
            {
                options.UseNpgsql(GetConnectionString())
                     .EnableDetailedErrors()
                     .EnableSensitiveDataLogging();
            });
        });
    }

    public async Task InitializeAsync()
    {
        if (UseHostDb) return;

        await _postgreSqlContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return UseHostDb
            ? Task.CompletedTask
            : _postgreSqlContainer.DisposeAsync().AsTask();
    }

    private string GetConnectionString()
    {
        return UseHostDb
            ? "Host=localhost;Database=MeasurementsDB;Username=measurements_user;Password=measurements_pass"
            : _postgreSqlContainer.GetConnectionString();
    }
}
