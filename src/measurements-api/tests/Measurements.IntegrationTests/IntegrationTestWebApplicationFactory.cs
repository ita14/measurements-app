using DotNet.Testcontainers;
using Measurements.Api.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Respawn;
using Testcontainers.CosmosDb;

namespace Measurements.IntegrationTests;

// ReSharper disable once ClassNeverInstantiated.Global
public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly CosmosDbContainer _cosmosDbContainer;
    private DatabaseFacade? _database;

    public IntegrationTestWebApplicationFactory()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Debug)
                .AddConsole();
        });
        var logger = loggerFactory.CreateLogger("test-containers");
        ConsoleLogger.Instance.DebugLogLevelEnabled = true;

        _cosmosDbContainer = new CosmosDbBuilder()
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
                options.UseCosmos(_cosmosDbContainer.GetConnectionString(), "MeasurementsDB")
                     .EnableDetailedErrors()
                     .EnableSensitiveDataLogging();
            });

            // Build an intermediate service provider.
            var serviceProvider = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context.
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MeasurementsDbContext>();

            // Ensure the database is created.
            db.Database.EnsureCreated();

            _database = db.Database;
        });
    }

    public async Task ResetDataAsync()
    {
        if (_database is null)
        {
            // Database is not yet initialized
            return;
        }

        var connection = _database.GetDbConnection();

        await connection.OpenAsync();

        var reSpawner = await Respawner.CreateAsync(connection);

        await reSpawner.ResetAsync(connection);

        await connection.CloseAsync();
    }

    public async Task InitializeAsync()
    {
        await _cosmosDbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
