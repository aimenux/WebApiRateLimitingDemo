using Example02.Presentation.Extensions;

namespace Example02.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddAuthorization();
        services.AddEndpoints();
        builder.AddRateLimiting();
        builder.AddSwaggerDoc();
        builder.AddLogging();
        return services;
    }
}