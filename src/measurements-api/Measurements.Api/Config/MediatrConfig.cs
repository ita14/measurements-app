using System.Reflection;
using MediatR;

namespace Measurements.Api.Config;

public static class MediatrConfig
{
    public static void SetupMediatr(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Infrastructure.Behaviors.ValidationBehavior<,>));
    }
}
