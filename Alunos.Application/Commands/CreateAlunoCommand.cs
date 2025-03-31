using MediatR;

namespace Alunos.Application.Commands;

public class CreateAlunoCommand : IRequest<Guid>
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public Guid TurmaId { get; set; }
}
