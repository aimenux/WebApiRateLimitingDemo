namespace Example01.Domain.ValueObjects;

public sealed record City(string Name)
{
    public static implicit operator City(string name) => new(name);
}