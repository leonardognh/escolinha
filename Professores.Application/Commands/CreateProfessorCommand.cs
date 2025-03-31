using MediatR;
using Professores.Application.DTOs;

namespace Professores.Application.Commands;

public class CreateProfessorCommand : IRequest<Guid>
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefone { get; set; }
}
