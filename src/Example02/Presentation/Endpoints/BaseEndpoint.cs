namespace Example02.Presentation.Endpoints;

public interface IEndpoint
{
    RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app);
}

public abstract class BaseEndpoint : IEndpoint
{
    protected virtual string RouteName => "api/weather";

    public abstract RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app);
}