using MediatR;
using Microsoft.AspNetCore.Mvc;
using Turmas.Application.Commands;
using Turmas.Application.DTOs;
using Turmas.Application.Queries;

namespace Turmas.API.Controllers;

/// <summary>
/// Gerencia as turmas da escola.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TurmasController : ControllerBase
{
    private readonly IMediator _mediator;

    public TurmasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria uma nova turma.
    /// </summary>
    /// <param name="command">Dados da nova turma.</param>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateTurmaCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    /// <summary>
    /// Retorna uma turma específica pelo ID.
    /// </summary>
    /// <param name="id">Identificador da turma.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TurmaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetTurmaByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Retorna todas as turmas cadastradas com paginação.
    /// </summary>
    /// <param name="page">Número da página.</param>
    /// <param name="pageSize">Itens por página.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TurmaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllTurmasQuery(page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Atualiza uma turma existente.
    /// </summary>
    /// <param name="id">ID da turma a ser atualizada.</param>
    /// <param name="command">Dados atualizados da turma.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTurmaCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Remove uma turma pelo ID.
    /// </summary>
    /// <param name="id">ID da turma.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteTurmaCommand(id));
        return NoContent();
    }
}
