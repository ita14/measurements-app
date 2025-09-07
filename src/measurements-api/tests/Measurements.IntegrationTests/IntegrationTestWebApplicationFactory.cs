using System.Security.Claims;
using System.Text.Encodings.Web;
using DotNet.Testcontainers;
using DotNet.Testcontainers.Builders;
using Measurements.Api.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        builder.UseEnvironment("Test");
        
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<MeasurementsDbContext>));

            services.AddDbContext<MeasurementsDbContext>(options =>
            {
                options.UseNpgsql(GetConnectionString())
                     .EnableDetailedErrors()
                     .EnableSensitiveDataLogging();
            });

            // Add authentication for tests (using Test scheme)
            services.AddAuthentication("Test")
                .AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
            services.AddAuthorization();
        });
        
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "SeedData", "false" },
                { "PostgreSQL:Host", "localhost" },
                { "PostgreSQL:Port", "5432" },
                { "PostgreSQL:Database", "MeasurementsDB" },
                { "PostgreSQL:Username", "measurements_user" },
                { "PostgreSQL:Password", "measurements_pass" }
            });
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = builder.Build();
        host.Start();
        return host;
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

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim(ClaimTypes.NameIdentifier, "123"),
        };

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
