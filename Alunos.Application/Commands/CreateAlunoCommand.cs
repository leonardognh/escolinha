using MediatR;

namespace Alunos.Application.Commands;

public class CreateAlunoCommand : IRequest<Guid>
{
    public string Nome { get; set; } = string.Empty;
    public Guid TurmaId { get; set; }
}
