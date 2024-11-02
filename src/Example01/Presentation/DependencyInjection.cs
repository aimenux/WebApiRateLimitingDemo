using Example01.Presentation.Extensions;

namespace Example01.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddControllers();
        builder.AddRateLimiting();
        builder.AddSwaggerDoc();
        builder.AddLogging();
        return services;
    }
}