using Example01.Application.Abstractions;
using Example01.Domain.Entities;
using Example01.Domain.Enums;
using Example01.Domain.ValueObjects;

namespace Example01.Infrastructure.Proxies;

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