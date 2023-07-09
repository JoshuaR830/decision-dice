using decision_dice.Motivators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace decision_dice.Controllers;

[ApiController]
[Route("[controller]")]
public class MotivatorController : ControllerBase
{
    IMediator _mediator;

    public MotivatorController(IMediator mediatR)
    {
        this._mediator = mediatR;
    }

    [HttpGet(Name="GetMotivator")]
    public async Task<IActionResult> GetMotivator()
    {
        var motivator = await _mediator.Send(new MotivatorQuery());
        return Ok(motivator);
    }

}
