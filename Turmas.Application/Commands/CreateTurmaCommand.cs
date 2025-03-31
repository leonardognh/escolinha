using MediatR;

namespace Turmas.Application.Commands;

public class CreateTurmaCommand : IRequest<Guid>
{
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }
    public string? Turno { get; set; }
}
