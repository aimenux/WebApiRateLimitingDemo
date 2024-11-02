using MediatR;

namespace Example01.Application.Features.GetWeather;

public sealed record GetWeatherQuery(string City) : IRequest<GetWeatherQueryResponse>;