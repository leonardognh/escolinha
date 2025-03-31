using MediatR;

namespace Alunos.Application.Commands;

public class DeleteAlunoCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteAlunoCommand(Guid id) => Id = id;
}
