using MediatR;
using Turmas.Application.Commands;
using Turmas.Domain.Interfaces;

namespace Turmas.Application.Handlers;

public class DeleteTurmaHandler : IRequestHandler<DeleteTurmaCommand>
{
    private readonly ITurmaRepository _repository;

    public DeleteTurmaHandler(ITurmaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteTurmaCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
