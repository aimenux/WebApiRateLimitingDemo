using Example01.Domain.ValueObjects;

namespace Example01.Domain.Entities;

public sealed record Weather
{
    public required City City { get; init; }
    
    public required Temperature Temperature { get; init; }
}