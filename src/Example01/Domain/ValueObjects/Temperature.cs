using Example01.Domain.Enums;

namespace Example01.Domain.ValueObjects;

public sealed record Temperature(double Value, TemperatureType Type);