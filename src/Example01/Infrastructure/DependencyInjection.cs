using Example01.Application.Abstractions;
using Example01.Infrastructure.Proxies;

namespace Example01.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IWeatherProxy, WeatherProxy>();
        return services;
    }
}