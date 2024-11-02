using FluentValidation;

namespace Example01.Application.Features.GetWeather;

public class GetWeatherQueryValidator : AbstractValidator<GetWeatherQuery>
{
    public GetWeatherQueryValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty();
    }
}