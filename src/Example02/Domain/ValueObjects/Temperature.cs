using Example02.Domain.Enums;

namespace Example02.Domain.ValueObjects;

public sealed record Temperature(double Value, TemperatureType Type);