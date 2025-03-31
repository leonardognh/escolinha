using MediatR;

namespace Professores.Application.Commands;

public class DeleteProfessorCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteProfessorCommand(Guid id)
    {
        Id = id;
    }
}
