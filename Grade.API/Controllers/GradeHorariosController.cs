using Grade.Application.Commands;
using Grade.Application.DTOs;
using Grade.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Grade.API.Controllers;

/// <summary>
/// Gerencia a grade horária bimestral da escola.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GradeHorariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public GradeHorariosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria uma nova grade horária.
    /// </summary>
    /// <param name="command">Dados da grade horária.</param>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateGradeHorariosCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    /// <summary>
    /// Retorna uma grade horária pelo ID.
    /// </summary>
    /// <param name="id">Identificador da grade horária.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GradeHorariosDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetGradeHorariosByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Retorna todas as grades horárias com paginação.
    /// </summary>
    /// <param name="page">Número da página.</param>
    /// <param name="pageSize">Itens por página.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GradeHorariosDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllGradeHorariossQuery(page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Atualiza uma grade horária existente.
    /// </summary>
    /// <param name="id">ID da grade horária.</param>
    /// <param name="command">Dados atualizados.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGradeHorariosCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Remove uma grade horária pelo ID.
    /// </summary>
    /// <param name="id">ID da grade horária.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteGradeHorariosCommand(id));
        return NoContent();
    }
}
