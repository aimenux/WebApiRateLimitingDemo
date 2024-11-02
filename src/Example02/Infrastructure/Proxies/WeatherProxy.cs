using Example02.Application.Abstractions;
using Example02.Domain.Entities;
using Example02.Domain.Enums;
using Example02.Domain.ValueObjects;

namespace Example02.Infrastructure.Proxies;

public sealed class WeatherProxy : IWeatherProxy
{
    public Task<Weather> GetWeatherAsync(string city, CancellationToken cancellationToken)
    {
        var weather = new Weather
        {
            City = city,
            Temperature = new Temperature(25, TemperatureType.Celsius),
            Description = "Sunny"
        };

        return Task.FromResult(weather);
    }
}