using Example02.Domain.Entities;
using Example02.Domain.ValueObjects;

namespace Example02.Application.Abstractions;

public interface IWeatherProxy
{
    Task<Weather> GetWeatherAsync(City city, CancellationToken cancellationToken);
}