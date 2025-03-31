using MediatR;
using Turmas.Application.Commands;
using Turmas.Domain.Entities;
using Turmas.Domain.Interfaces;
using MassTransit;
using IntegracaoMicroservicos.Contracts.Events.Turmas;

namespace Turmas.Application.Handlers;

public class CreateTurmaHandler : IRequestHandler<CreateTurmaCommand, Guid>
{
    private readonly ITurmaRepository _repository;
    private readonly IPublishEndpoint _publish;

    public CreateTurmaHandler(ITurmaRepository repository, IPublishEndpoint publish)
    {
        _repository = repository;
        _publish = publish;
    }

    public async Task<Guid> Handle(CreateTurmaCommand request, CancellationToken cancellationToken)
    {
        var turma = new Turma
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Ano = request.Ano,
            Turno = request.Turno
        };

        await _repository.AddAsync(turma);

        await _publish.Publish(new TurmaCriadaEvent(
            turma.Id, turma.Nome, turma.Ano, turma.Turno
        ));

        return turma.Id;
    }
}
