using IntegracaoMicroservicos.Contracts.Events;
using MassTransit;
using MediatR;
using Professores.Application.Commands;
using Professores.Domain.Entities;
using Professores.Domain.Interfaces;

namespace Professores.Application.Handlers;

public class CreateProfessorHandler : IRequestHandler<CreateProfessorCommand, Guid>
{
    private readonly IProfessorRepository _repository;
    private readonly IPublishEndpoint _publish;

    public CreateProfessorHandler(IProfessorRepository repository, IPublishEndpoint publish)
    {
        _repository = repository;
        _publish = publish;
    }

    public async Task<Guid> Handle(CreateProfessorCommand request, CancellationToken cancellationToken)
    {
        var professor = new Professor
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Email = request.Email,
            Telefone = request.Telefone
        };

        await _repository.AddAsync(professor);

        await _publish.Publish(new ProfessorCriadoEvent(professor.Id, professor.Nome, professor.Email));

        return professor.Id;
    }
}
