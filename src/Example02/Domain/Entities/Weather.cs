using Example02.Domain.ValueObjects;

namespace Example02.Domain.Entities;

public sealed record Weather
{
    public required City City { get; init; }
    
    public required Temperature Temperature { get; init; }
}