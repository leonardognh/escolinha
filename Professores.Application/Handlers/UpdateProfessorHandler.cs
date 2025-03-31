using MediatR;
using Professores.Application.Commands;
using Professores.Domain.Entities;
using Professores.Domain.Interfaces;

namespace Professores.Application.Handlers;

public class UpdateProfessorHandler : IRequestHandler<UpdateProfessorCommand>
{
    private readonly IProfessorRepository _repository;

    public UpdateProfessorHandler(IProfessorRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateProfessorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity is null) throw new Exception("Professor não encontrado.");

        entity.Nome = request.Nome;
        entity.Email = request.Email;
        entity.Telefone = request.Telefone;

        await _repository.UpdateAsync(entity);
        return Unit.Value;
    }
}
