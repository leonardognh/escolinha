using MediatR;
using Microsoft.AspNetCore.Mvc;
using Professores.Application.Commands;
using Professores.Application.DTOs;
using Professores.Application.Queries;

namespace Professores.API.Controllers;

/// <summary>
/// Gerencia professores da escola.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProfessoresController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfessoresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retorna todos os professores cadastrados com paginação.
    /// </summary>
    /// <param name="page">Número da página.</param>
    /// <param name="pageSize">Quantidade de itens por página.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProfessorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllProfessoresQuery(page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Retorna um professor específico pelo ID.
    /// </summary>
    /// <param name="id">Identificador do professor.</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProfessorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProfessorByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Cria um novo professor.
    /// </summary>
    /// <param name="command">Objeto com os dados do professor.</param>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProfessorCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    /// <summary>
    /// Atualiza um professor existente.
    /// </summary>
    /// <param name="id">ID do professor a ser atualizado.</param>
    /// <param name="command">Dados atualizados do professor.</param>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProfessorCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Remove um professor pelo ID.
    /// </summary>
    /// <param name="id">ID do professor.</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProfessorCommand(id));
        return NoContent();
    }
}