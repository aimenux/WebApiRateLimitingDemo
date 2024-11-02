using MediatR;

namespace Example02.Application.Features.GetWeather;

public sealed record GetWeatherQuery(string City) : IRequest<GetWeatherQueryResponse>;