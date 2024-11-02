namespace Example01.Presentation.Controllers.GetWeather;

public sealed record GetWeatherResponse(string City, double Temperature, string Description);