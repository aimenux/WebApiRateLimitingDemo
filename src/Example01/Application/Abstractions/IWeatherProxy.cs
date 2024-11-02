using Example01.Domain.Entities;

namespace Example01.Application.Abstractions;

public interface IWeatherProxy
{
    Task<Weather> GetWeatherAsync(string city, CancellationToken cancellationToken);
}