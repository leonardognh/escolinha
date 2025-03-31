using MediatR;

namespace Turmas.Application.Commands;

public class UpdateTurmaCommand : IRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }
    public string? Turno { get; set; }
}
