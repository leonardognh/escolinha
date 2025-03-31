using MediatR;
using Professores.Application.Commands;
using Professores.Domain.Interfaces;

namespace Professores.Application.Handlers;

public class DeleteProfessorHandler : IRequestHandler<DeleteProfessorCommand>
{
    private readonly IProfessorRepository _repository;

    public DeleteProfessorHandler(IProfessorRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteProfessorCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
