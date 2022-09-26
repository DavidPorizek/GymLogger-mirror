using GymLogger.API.Handler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymLogger.API.Controllers;

[Route("api/Configurations2")]
[ApiController]
public class ConfigurationController2 : ControllerBase
{
    private readonly IMediator _mediator;
    public ConfigurationController2(IMediator mediator) => _mediator = mediator;

    // GET: api/Configurations
    [HttpGet]
    public async Task<IActionResult> GetConfigurations()
    {
        var result = await _mediator.Send(new GetConfigurationsQuery());
        return Ok(result);
    }
}
