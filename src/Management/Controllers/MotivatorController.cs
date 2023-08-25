using Application.Motivators.Commands;
using Application.Motivators.Models;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("[controller]")]
public class MotivatorController : ControllerBase
{
    IMediator _mediator;

    public MotivatorController(IMediator mediatR) =>
        _mediator = mediatR;

    [HttpGet(Name="GetMotivator")]
    public async Task<IActionResult> GetMotivator()
    {
        var motivator = await _mediator.Send(new MotivatorQuery());
        return Ok(motivator);
    }

    [HttpPost(Name="CreateMotivator")]
    public async Task<IActionResult> CreateMotivator()
    {
        var motivator = new Motivator(Guid.NewGuid(), "", "", "", "");
        await _mediator.Send(new CreateMotivatorCommand(motivator));

        return CreatedAtAction(nameof(GetMotivator), new { id = motivator.Id }, motivator);
    }
}
