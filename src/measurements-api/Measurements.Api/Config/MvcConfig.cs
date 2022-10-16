using OpenApi.Measurements.Api;
using MeasurementsController = Measurements.Api.Controllers.MeasurementsController;
using SensorsController = Measurements.Api.Controllers.SensorsController;

namespace Measurements.Api.Config;

public static class MvcConfig
{
    public static void SetupControllers(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddScoped<IMeasurementsController, MeasurementsController>();
        services.AddScoped<ISensorsController, SensorsController>();
    }
}
