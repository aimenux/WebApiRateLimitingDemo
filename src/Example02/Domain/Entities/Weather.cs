﻿using Example02.Domain.ValueObjects;

namespace Example02.Domain.Entities;

public sealed record Weather
{
    public required string City { get; init; }
    
    public required Temperature Temperature { get; init; }
    
    public required string Description { get; init; }
}