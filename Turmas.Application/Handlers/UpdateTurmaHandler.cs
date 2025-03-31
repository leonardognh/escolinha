using MediatR;
using Turmas.Application.Commands;
using Turmas.Domain.Interfaces;
using MassTransit;
using Contracts.Events.Turmas;

namespace Turmas.Application.Handlers;

public class UpdateTurmaHandler : IRequestHandler<UpdateTurmaCommand>
{
    private readonly ITurmaRepository _repository;
    private readonly IPublishEndpoint _publish;

    public UpdateTurmaHandler(ITurmaRepository repository, IPublishEndpoint publish)
    {
        _repository = repository;
        _publish = publish;
    }

    public async Task<Unit> Handle(UpdateTurmaCommand request, CancellationToken cancellationToken)
    {
        var turma = await _repository.GetByIdAsync(request.Id)
            ?? throw new Exception("Turma não encontrada.");

        turma.Nome = request.Nome;
        turma.Ano = request.Ano;
        turma.Turno = request.Turno;

        await _repository.UpdateAsync(turma);

        await _publish.Publish(new TurmaAtualizadaEvent(
            turma.Id, turma.Nome, turma.Ano, turma.Turno
        ));

        return Unit.Value;
    }
}
