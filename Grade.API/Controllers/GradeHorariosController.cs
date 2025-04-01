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
    /// Retorna uma lista paginada de grades horárias.
    /// </summary>
    /// <param name="page">Número da página (padrão: 1).</param>
    /// <param name="pageSize">Tamanho da página (padrão: 10).</param>
    /// <returns>Lista paginada de grades horárias.</returns>
    /// <response code="200">Lista obtida com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GradeHorariosDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllGradeHorariosQuery(page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Obtém uma grade horária pelo seu identificador.
    /// </summary>
    /// <param name="id">ID da grade horária.</param>
    /// <returns>Detalhes da grade horária.</returns>
    /// <response code="200">Grade horária encontrada.</response>
    /// <response code="404">Grade horária não encontrada.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GradeHorariosDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetGradeHorariosByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Cria uma nova grade horária.
    /// </summary>
    /// <param name="command">Dados da grade horária a ser criada.</param>
    /// <returns>ID da grade horária criada.</returns>
    /// <response code="201">Grade horária criada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateGradeHorariosCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    /// <summary>
    /// Atualiza uma grade horária existente.
    /// </summary>
    /// <param name="id">ID da grade horária.</param>
    /// <param name="command">Dados atualizados da grade horária.</param>
    /// <response code="204">Atualizado com sucesso.</response>
    /// <response code="400">ID do corpo e da URL não coincidem.</response>
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
    /// Exclui uma grade horária pelo ID.
    /// </summary>
    /// <param name="id">ID da grade horária.</param>
    /// <response code="204">Excluída com sucesso.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteGradeHorariosCommand(id));
        return NoContent();
    }
}
