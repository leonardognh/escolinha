using MediatR;
using Microsoft.AspNetCore.Mvc;
using Professores.Application.Commands;
using Professores.Application.Queries;

namespace Professores.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessoresController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfessoresController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllProfessoresQuery(page, pageSize));
        return Ok(result);
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProfessorByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProfessorCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProfessorCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProfessorCommand(id));
        return NoContent();
    }
}
