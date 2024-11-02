using System.Reflection;
using Example02.Presentation.Endpoints;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Example02.Presentation.Extensions;

public static class EndpointsExtensions
{
    private static readonly Type EndpointType = typeof(IEndpoint);
    
    private static readonly Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
    
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var serviceDescriptors = CurrentAssembly
            .GetTypes()
            .Where(IsEndpointType)
            .Select(type => ServiceDescriptor.Transient(EndpointType, type));
        services.TryAddEnumerable(serviceDescriptors);
        return services;
    }
    
    public static IApplicationBuilder MapEndpoints(this WebApplication app, bool requireRateLimiting)
    {
        var configuration = app.Services.GetRequiredService<IConfiguration>();
        var endpoints = app.Services.GetServices<IEndpoint>();
        foreach (var endpoint in endpoints)
        {
            var builder = endpoint.MapEndpoint(app);
            if (requireRateLimiting)
            {
                builder.RequireRateLimiting(configuration);
            }
        }
        return app;
    }

    private static bool IsEndpointType(Type type)
    {
        return EndpointType.IsAssignableFrom(type) && type is { IsClass: true, IsAbstract: false };
    }
}