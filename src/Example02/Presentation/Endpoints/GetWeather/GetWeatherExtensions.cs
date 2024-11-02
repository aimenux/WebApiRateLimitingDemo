using Example02.Application.Features.GetWeather;

namespace Example02.Presentation.Endpoints.GetWeather;

public static class GetWeatherExtensions
{
    public static GetWeatherQuery ToQuery(this GetWeatherRequest request)
    {
        return new GetWeatherQuery(request.City);
    }
    
    public static GetWeatherResponse ToResponse(this GetWeatherQueryResponse response)
    {
        return new GetWeatherResponse(response.Weather.City, response.Weather.Temperature.Value, response.Weather.Description);
    }
}