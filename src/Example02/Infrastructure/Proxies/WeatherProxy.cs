using Example02.Application.Abstractions;
using Example02.Domain.Entities;
using Example02.Domain.Enums;
using Example02.Domain.ValueObjects;

namespace Example02.Infrastructure.Proxies;

public sealed class WeatherProxy : IWeatherProxy
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