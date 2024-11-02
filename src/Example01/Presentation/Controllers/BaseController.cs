using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example01.Presentation.Controllers;

[ApiController]
[Route("api/weather")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public abstract class BaseController : ControllerBase
{
    protected readonly ISender Sender;

    protected BaseController(ISender sender)
    {
        Sender = sender;
    }
}