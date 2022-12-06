using System.Reflection;
using Hellang.Middleware.ProblemDetails;
using Measurements.Api.Config;
using Measurements.Api.Extensions;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.SetupCosmosDb(builder.Configuration);
builder.Services.SetupMediatr();
builder.Services.SetupValidation();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Host.UseSerilog();

if (builder.Environment.IsProduction())
{
    builder.AddKeycloakAuth();
}

try
{
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.EnsureCosmosDbIsCreated().Wait();
        app.SeedTestDataIfEmptyAsync().Wait();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles(new StaticFileOptions()
    {
        ContentTypeProvider = new FileExtensionContentTypeProvider()
        {
            Mappings =
            {
                [".yaml"] ="text/yaml"
            }
        }
    });

    app.UseProblemDetails();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/measurements-api.yaml", "Measurements API");
    });

    _ = app.Environment.IsDevelopment()
        ? app.MapControllers().AllowAnonymous()
        : app.MapControllers().RequireAuthorization();

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
