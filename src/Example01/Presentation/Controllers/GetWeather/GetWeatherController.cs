using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example01.Presentation.Controllers.GetWeather;

public sealed class GetWeatherController : BaseController
{
    public GetWeatherController(ISender sender) : base(sender)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult> GetWeather([FromQuery] GetWeatherRequest request, CancellationToken cancellationToken)
    {
        var query = request.ToQuery();
        var queryResponse = await Sender.Send(query, cancellationToken);
        var apiResponse = queryResponse.ToResponse();
        return Ok(apiResponse);
    }
}