using MediatR;

namespace Professores.Application.Commands;

public class UpdateProfessorCommand : IRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefone { get; set; }
}
