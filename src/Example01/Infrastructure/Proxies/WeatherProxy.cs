using Example01.Application.Abstractions;
using Example01.Domain.Entities;
using Example01.Domain.Enums;
using Example01.Domain.ValueObjects;

namespace Example01.Infrastructure.Proxies;

internal sealed class WeatherProxy : IWeatherProxy
{
    public async Task<Weather> GetWeatherAsync(City city, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        
        var weather = new Weather
        {
            City = city,
            Temperature = new Temperature(25, TemperatureType.Celsius)
        };

        return weather;
    }
}