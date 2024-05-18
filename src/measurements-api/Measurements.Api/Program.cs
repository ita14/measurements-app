using System.Reflection;
using Hellang.Middleware.ProblemDetails;
using Measurements.Api.Config;
using Measurements.Api.Domain.Interfaces;
using Measurements.Api.Extensions;
using Measurements.Api.Infrastructure.AppSettings;
using Measurements.Api.Infrastructure.Extensions;
using Measurements.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddCosmosDb(builder.Configuration.GetSection("CosmosDB").Get<CosmosDbSettings>());
builder.Services.SetupMediatr();
builder.Services.SetupValidation();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
builder.Host.UseSerilog();

if (builder.Environment.IsProduction())
{
    builder.AddKeycloakAuth();
}

try
{
    WebApplication app = builder.Build();

    // TODO: Seed only for dev environment.
    app.EnsureDbCreated();
    app.SeedTestDataIfEmptyAsync().Wait();

    app.UseHttpsRedirection();
    app.UseStaticFiles(new StaticFileOptions
    {
        ContentTypeProvider = new FileExtensionContentTypeProvider { Mappings = { [".yaml"] = "text/yaml" } }
    });

    app.UseProblemDetails();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

    _ = app.Environment.IsDevelopment()
        ? app.MapControllers().AllowAnonymous()
        : app.MapControllers().RequireAuthorization();

    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/measurements-api.yaml", "Measurements API"); });

    Log.Information("Starting up...");
    app.Run();
    Log.Information("Shutting down...");
}
catch (Exception e)
{
    Log.Fatal(e, "Host terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}

// Allow integration tests to import the class
// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program
{
}
