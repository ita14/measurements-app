using DotNet.Testcontainers;
using DotNet.Testcontainers.Builders;
using Measurements.Api.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Azure.Cosmos;
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
            .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged(@"Started\r?\n"))
            .WithEnvironment("AZURE_COSMOS_EMULATOR_PARTITION_COUNT", "3")
            .WithEnvironment("AZURE_COSMOS_EMULATOR_IP_ADDRESS_OVERRIDE", "127.0.0.1")
            .WithEnvironment("AZURE_COSMOS_EMULATOR_ENABLE_DATA_PERSISTENCE", "true")
            .WithExposedPort(8081)
            .WithExposedPort(10251)
            .WithExposedPort(10252)
            .WithExposedPort(10253)
            .WithExposedPort(10254)
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
                options.UseCosmos(
                        _cosmosDbContainer.GetConnectionString(),
                        "MeasurementsDB",
                        optionsBuilder =>
                        {
                            optionsBuilder.ConnectionMode(ConnectionMode.Gateway);
                            optionsBuilder.LimitToEndpoint();
                            optionsBuilder.HttpClientFactory(() =>
                            {
                                HttpMessageHandler httpMessageHandler = new HttpClientHandler()
                                {
                                    ServerCertificateCustomValidationCallback = HttpClientHandler
                                        .DangerousAcceptAnyServerCertificateValidator
                                };

                                return new HttpClient(httpMessageHandler);
                            });
                        })
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
        return _cosmosDbContainer.DisposeAsync().AsTask();
    }
}
