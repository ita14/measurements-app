using DotNet.Testcontainers;
using DotNet.Testcontainers.Builders;
using Measurements.Api.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.CosmosDb;

namespace Measurements.IntegrationTests;

// ReSharper disable once ClassNeverInstantiated.Global
public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private const bool UseHostDb = false;
    private readonly CosmosDbContainer _cosmosDbContainer;

    public IntegrationTestWebApplicationFactory()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Debug)
                .AddConsole();
        });
        var logger = loggerFactory.CreateLogger("test-containers");
        ConsoleLogger.Instance.DebugLogLevelEnabled = true;

        // TODO: Container is super slow to start. Wait check using "Started" log is not enough
        // but we should poll the /_explorer/emulator.pem path. UntilHttpRequestIsSucceeded is not
        // usable here because of the cert issue. Should probably use custom wait strategy with
        // HttpClient skipping cert validation. For now just sleep after container up.
        _cosmosDbContainer = new CosmosDbBuilder()
            .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged(@"Started\r?\n"))
            //.WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(request => request.ForPath("/_explorer/emulator.pem").ForPort(8081).UsingTls()))
            .WithEnvironment("AZURE_COSMOS_EMULATOR_PARTITION_COUNT", "3")
            .WithPortBinding(8081, true)
            .WithPortBinding(10251, 10251)
            .WithPortBinding(10252, 10252)
            .WithPortBinding(10253, 10253)
            .WithPortBinding(10254, 10254)
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
                        GetConnectionString(),
                        "MeasurementsDB",
                        optionsBuilder =>
                        {
                            optionsBuilder.ConnectionMode(ConnectionMode.Gateway);
                            optionsBuilder.LimitToEndpoint();
                            optionsBuilder.HttpClientFactory(() =>
                            {
                                var handler = new HttpClientHandler
                                {
                                    ServerCertificateCustomValidationCallback =
                                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                                };
                                return new HttpClient(handler);
                            });
                        })
                     .EnableDetailedErrors()
                     .EnableSensitiveDataLogging();
            });
        });
    }

    public async Task InitializeAsync()
    {
        if (UseHostDb) return;

        await _cosmosDbContainer.StartAsync();

        // See comments from container build.
        await Task.Delay(TimeSpan.FromMinutes(3));
    }

    public new Task DisposeAsync()
    {
        return UseHostDb
            ? Task.CompletedTask
            : _cosmosDbContainer.DisposeAsync().AsTask();
    }

    private string GetConnectionString()
    {
        return UseHostDb
            ? "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
            : _cosmosDbContainer.GetConnectionString();
    }
}
