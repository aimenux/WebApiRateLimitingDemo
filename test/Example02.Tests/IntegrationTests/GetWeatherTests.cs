using System.Net;
using Example02.Tests.IntegrationTests.Helpers;
using AwesomeAssertions;

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
        var response = await client.GetAsync(new Uri(route, UriKind.Relative));
        var responseBody = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseBody.Should().NotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public async Task Should_Get_Weather_Returns_TooManyRequests()
    {
        // arrange
        var client = _factory.CreateClient();

        // act
        var tasks = Enumerable.Range(0, 10)
            .Select(_ => client.GetAsync(new Uri("api/weather?city=paris", UriKind.Relative)))
            .ToArray();
        var responses = await Task.WhenAll(tasks);
        
        // assert
        responses.Any(r => r.StatusCode == HttpStatusCode.TooManyRequests).Should().BeTrue();
    }
}