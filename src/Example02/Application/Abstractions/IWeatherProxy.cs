using Example02.Domain.Entities;

namespace Example02.Application.Abstractions;

public interface IWeatherProxy
{
    Task<Weather> GetWeatherAsync(string city, CancellationToken cancellationToken);
}