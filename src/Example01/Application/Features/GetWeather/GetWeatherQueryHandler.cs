using Example01.Application.Abstractions;
using MediatR;

namespace Example01.Application.Features.GetWeather;

public sealed class GetWeatherQueryHandler : IRequestHandler<GetWeatherQuery, GetWeatherQueryResponse>
{
    private readonly IWeatherProxy _weatherProxy;

    public GetWeatherQueryHandler(IWeatherProxy weatherProxy)
    {
        _weatherProxy = weatherProxy;
    }

    public async Task<GetWeatherQueryResponse> Handle(GetWeatherQuery request, CancellationToken cancellationToken)
    {
        var weather = await _weatherProxy.GetWeatherAsync(request.City, cancellationToken);
        return new GetWeatherQueryResponse(weather);
    }
}