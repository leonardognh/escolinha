using MediatR;
using Microsoft.AspNetCore.Mvc;
using Materias.Application.Commands;
using Materias.Application.Queries;
using Materias.Application.DTOs;

namespace Materias.API.Controllers;

/// <summary>
/// Gerencia as matérias da grade curricular.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MateriasController : ControllerBase
{
    private readonly IMediator _mediator;

    public MateriasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria uma nova matéria.
    /// </summary>
    /// <param name="command">Dados da nova matéria.</param>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateMateriaCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    /// <summary>
    /// Retorna uma matéria específica pelo ID.
    /// </summary>
    /// <param name="id">Identificador da matéria.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MateriaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetMateriaByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Retorna todas as matérias cadastradas com paginação.
    /// </summary>
    /// <param name="page">Número da página.</param>
    /// <param name="pageSize">Itens por página.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MateriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllMateriasQuery(page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Atualiza uma matéria existente.
    /// </summary>
    /// <param name="id">ID da matéria.</param>
    /// <param name="command">Dados atualizados da matéria.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMateriaCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Remove uma matéria pelo ID.
    /// </summary>
    /// <param name="id">ID da matéria.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteMateriaCommand(id));
        return NoContent();
    }
}
