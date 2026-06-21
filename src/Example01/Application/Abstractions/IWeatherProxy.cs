using Example01.Domain.Entities;
using Example01.Domain.ValueObjects;

namespace Example01.Application.Abstractions;

public interface IWeatherProxy
{
    Task<Weather> GetWeatherAsync(City city, CancellationToken cancellationToken);
}