using Serilog;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

try
{
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.UseStaticFiles();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/measurements-api.yaml", "Measurements API");
    });

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
