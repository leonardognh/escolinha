using Grade.Application.Commands;
using Grade.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Grade.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeHorarioController : ControllerBase
{
    private readonly IMediator _mediator;

    public GradeHorarioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGradeHorarioCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetGradeHorarioByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllGradeHorariosQuery(page, pageSize));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGradeHorarioCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteGradeHorarioCommand(id));
        return NoContent();
    }
}
