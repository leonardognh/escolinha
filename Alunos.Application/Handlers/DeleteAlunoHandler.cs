using Alunos.Application.Commands;
using Alunos.Domain.Interfaces;
using MediatR;

namespace Alunos.Application.Handlers;

public class DeleteAlunoHandler : IRequestHandler<DeleteAlunoCommand>
{
    private readonly IAlunoRepository _repository;

    public DeleteAlunoHandler(IAlunoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteAlunoCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
