using MediatR;

namespace Alunos.Application.Commands;

public class UpdateAlunoCommand : IRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public Guid TurmaId { get; set; }
}
