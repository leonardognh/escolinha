using Grade.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Grade.Application.Queries;
using Grade.Application.DTOs;
using System.Text.Json;

namespace Grade.API.Controllers;

/// <summary>
/// Gerencia as relações entre Grade Horário e Matérias.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GradeHorarioMateriaController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GradeHorarioMateriaController> _logger;

    public GradeHorarioMateriaController(IMediator mediator, ILogger<GradeHorarioMateriaController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Obtém uma lista paginada de todas as relações GradeHorário-Matéria.
    /// </summary>
    /// <param name="page">Número da página (padrão: 1).</param>
    /// <param name="pageSize">Tamanho da página (padrão: 10).</param>
    /// <returns>Lista paginada de relações.</returns>
    /// <response code="200">Lista retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GradeHorarioMateriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllGradeHorarioMateriasQuery(page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Obtém uma relação específica entre GradeHorário e Matéria pelo ID.
    /// </summary>
    /// <param name="gradeHorarioId">ID da grade.</param>
    /// <returns>Detalhes da relação encontrada.</returns>
    /// <response code="200">Relação encontrada com sucesso.</response>
    /// <response code="404">Relação não encontrada.</response>
    [HttpGet("{gradeHorarioId}/{materiaId}/{professorId}")]
    [ProducesResponseType(typeof(GradeHorarioMateriaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid gradeHorarioId)
    {
        var result = await _mediator.Send(new GetGradeHorarioMateriaByIdQuery(gradeHorarioId));
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Cria uma nova relação entre GradeHorário e Matéria.
    /// </summary>
    /// <param name="command">Comando com os dados da nova relação.</param>
    /// <returns>ID da relação criada.</returns>
    /// <response code="201">Relação criada com sucesso.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateGradeHorarioMateriaCommand command)
    {
        _logger.LogInformation($"Creating new GradeHorarioMateria: {JsonSerializer.Serialize(command)}");
        var id = await _mediator.Send(command);
        _logger.LogInformation($"Created: {id}");
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    /// <summary>
    /// Atualiza uma relação existente entre GradeHorário e Matéria.
    /// </summary>
    /// <param name="id">ID da relação a ser atualizada.</param>
    /// <param name="command">Comando com os novos dados.</param>
    /// <returns>Resposta sem conteúdo.</returns>
    /// <response code="204">Relação atualizada com sucesso.</response>
    /// <response code="400">ID do caminho não corresponde ao do corpo da requisição.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGradeHorarioMateriaCommand command)
    {
        if (id != command.GradeHorarioId) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }
}
