using MediatR;

namespace Alunos.Application.Commands;

public class UpdateAlunoCommand : IRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public Guid TurmaId { get; set; }
}
