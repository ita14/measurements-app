using Measurements.Api.Application.Commands.Sensors;
using MediatR;

namespace Measurements.Api.Config;

public static class MediatrConfig
{
    public static void SetupMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateSensorCommand).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Infrastructure.Behaviors.ValidationBehavior<,>));
    }
}
