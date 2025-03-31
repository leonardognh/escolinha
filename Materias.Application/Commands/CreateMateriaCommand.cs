using MediatR;

namespace Materias.Application.Commands;

public class CreateMateriaCommand : IRequest<Guid>
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public int? CargaHoraria { get; set; }
}
