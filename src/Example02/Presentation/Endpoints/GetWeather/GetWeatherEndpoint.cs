using MediatR;

namespace Example02.Presentation.Endpoints.GetWeather;

public sealed class GetWeatherEndpoint : BaseEndpoint
{
    public override RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app
            .MapGet(RouteName, async (ISender sender, [AsParameters] GetWeatherRequest request, CancellationToken cancellationToken) =>
            {
                var query = request.ToQuery();
                var queryResponse = await sender.Send(query, cancellationToken);
                var apiResponse = queryResponse.ToResponse();
                return Results.Ok(apiResponse);
            });
    }
}