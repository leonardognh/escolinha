using MediatR;

namespace Materias.Application.Commands;

public class CreateMateriaCommand : IRequest<Guid>
{
    public string Nome { get; set; } = string.Empty;
}
