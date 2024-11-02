using Example02.Application.Abstractions;
using Example02.Application.Features.GetWeather;
using Example02.Domain.Entities;
using Example02.Domain.Enums;
using Example02.Domain.ValueObjects;
using FluentAssertions;
using NSubstitute;

namespace Example02.Tests.UnitTests;

public class GetWeatherQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_WeatherQueryResponse_When_City_Is_Valid()
    {
        // arrange
        var city = Guid.NewGuid().ToString();
        var query = new GetWeatherQuery(city);
        var weather = new Weather
        {
            City = city,
            Temperature = new Temperature(20, TemperatureType.Celsius),
            Description = Guid.NewGuid().ToString()
        };
        
        var proxy = Substitute.For<IWeatherProxy>();
        proxy
            .GetWeatherAsync(city, Arg.Any<CancellationToken>())
            .Returns(weather);
        
        var handler = new GetWeatherQueryHandler(proxy);

        // act
        var queryResponse = await handler.Handle(query, CancellationToken.None);

        // assert
        queryResponse.Should().NotBeNull();
        queryResponse.Weather.Should().NotBeNull();
        queryResponse.Weather.Should().BeEquivalentTo(weather);
    }
}