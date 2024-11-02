namespace Example02.Presentation.Endpoints.GetWeather;

public sealed record GetWeatherResponse(string City, double Temperature, string Description);