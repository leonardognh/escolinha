using MediatR;

namespace Turmas.Application.Commands;

public class DeleteTurmaCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteTurmaCommand(Guid id)
    {
        Id = id;
    }
}
