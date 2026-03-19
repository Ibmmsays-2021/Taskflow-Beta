using Application.Features.Tickets.Commands.CreateTicket;
using Application.Features.Tickets.Queries.GetTicketById.GetTicketByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketAPI(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateTicketCommand command)
    {
         return Ok(await _mediator.Send(command));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetTicketByIdQuery(id));
        return result is not null ? Ok(result) : NotFound();
    }
}
