using Alunos.Application.Commands;
using Alunos.Application.DTOs;
using Alunos.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alunos.API.Controllers;

/// <summary>
/// Gerencia os alunos da escola.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly IMediator _mediator;

    public AlunoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria um novo aluno.
    /// </summary>
    /// <param name="command">Dados do aluno a ser criado.</param>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAlunoCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    /// <summary>
    /// Retorna um aluno pelo ID.
    /// </summary>
    /// <param name="id">Identificador do aluno.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AlunoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetAlunoByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Retorna todos os alunos cadastrados com paginação.
    /// </summary>
    /// <param name="page">Número da página.</param>
    /// <param name="pageSize">Itens por página.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AlunoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllAlunosQuery(page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Atualiza um aluno existente.
    /// </summary>
    /// <param name="id">ID do aluno.</param>
    /// <param name="command">Dados atualizados do aluno.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAlunoCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Remove um aluno pelo ID.
    /// </summary>
    /// <param name="id">ID do aluno.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteAlunoCommand(id));
        return NoContent();
    }
}
