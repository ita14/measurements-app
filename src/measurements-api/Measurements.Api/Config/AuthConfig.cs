using Keycloak.AuthServices.Authentication;

namespace Measurements.Api.Config;

public static class AuthConfig
{
    public static void AddKeycloakAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddKeycloakAuthentication(builder.Configuration, o =>
        {
            o.RequireHttpsMetadata = false;
        });
    }
}
