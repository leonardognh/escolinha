using MediatR;

namespace Materias.Application.Commands;

public class UpdateMateriaCommand : IRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public int? CargaHoraria { get; set; }
}
