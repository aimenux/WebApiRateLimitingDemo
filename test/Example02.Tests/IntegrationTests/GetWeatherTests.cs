using System.Net;
using Example02.Tests.IntegrationTests.Helpers;
using FluentAssertions;

namespace Example02.Tests.IntegrationTests;

[Collection(IntegrationCollectionFixture.CollectionName)]
public class GetWeatherTests
{
    private readonly IntegrationWebApplicationFactory _factory;

    public GetWeatherTests(IntegrationWebApplicationFactory factory)
    {
        _factory = factory;
    }
    
    [Theory]
    [InlineData("api/weather?city=paris")]
    [InlineData("api/weather?city=tokyo")]
    public async Task Should_Get_Weather_Returns_Ok(string route)
    {
        // arrange
        var client = _factory.CreateClient();

        // act
        var response = await client.GetAsync(route);
        var responseBody = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseBody.Should().NotBeNullOrWhiteSpace();
    }
}