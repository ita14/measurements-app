using System.Reflection;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;

namespace Measurements.Api.Config;

public static class ValidationConfig
{
    public static void SetupValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddProblemDetails(o =>
        {
            o.ValidationProblemStatusCode = StatusCodes.Status400BadRequest;
            o.MapFluentValidationException();
            o.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
        });
    }

    private static void MapFluentValidationException(this ProblemDetailsOptions options) =>
        options.Map<ValidationException>((ctx, ex) =>
        {
            var factory = ctx.RequestServices.GetRequiredService<ProblemDetailsFactory>();

            var errors = ex.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(vf => vf.ErrorMessage).ToArray());

            return factory.CreateValidationProblemDetails(ctx, errors);
        });
}
