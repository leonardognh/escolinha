using MediatR;

namespace Professores.Application.Commands;

public class UpdateProfessorCommand : IRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}
