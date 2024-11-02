using Example02.Application.Abstractions;
using Example02.Infrastructure.Proxies;

namespace Example02.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IWeatherProxy, WeatherProxy>();
        return services;
    }
}