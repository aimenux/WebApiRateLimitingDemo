using Example02.Application.Features.GetWeather;
using FluentValidation.TestHelper;

namespace Example02.Tests.UnitTests;

public class GetWeatherQueryValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_City_Is_Empty()
    {
        // arrange
        var city = string.Empty;
        var query = new GetWeatherQuery(city);
        var validator = new GetWeatherQueryValidator();
        
        // act
        var result = validator.TestValidate(query);
        
        // assert
        result.ShouldHaveValidationErrorFor(x => x.City);
    }

    [Fact]
    public void Should_Not_Have_Error_When_City_Is_Not_Empty()
    {
        // arrange
        var city = Guid.NewGuid().ToString();
        var query = new GetWeatherQuery(city);
        var validator = new GetWeatherQueryValidator();
        
        // act
        var result = validator.TestValidate(query);
        
        // assert
        result.ShouldNotHaveValidationErrorFor(x => x.City);
    }
}