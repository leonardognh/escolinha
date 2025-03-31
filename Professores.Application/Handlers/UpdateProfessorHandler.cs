using IntegracaoMicroservicos.Contracts.Events.Professores;
using MassTransit;
using MediatR;
using Professores.Application.Commands;
using Professores.Domain.Interfaces;

public class UpdateProfessorHandler : IRequestHandler<UpdateProfessorCommand>
{
    private readonly IProfessorRepository _repository;
    private readonly IPublishEndpoint _publish;

    public UpdateProfessorHandler(IProfessorRepository repository, IPublishEndpoint publish)
    {
        _repository = repository;
        _publish = publish;
    }

    public async Task<Unit> Handle(UpdateProfessorCommand request, CancellationToken cancellationToken)
    {
        var professor = await _repository.GetByIdAsync(request.Id)
            ?? throw new Exception("Professor não encontrado.");

        professor.Nome = request.Nome;
        professor.Email = request.Email;

        await _repository.UpdateAsync(professor);

        // 🟢 Publica o evento de atualização
        await _publish.Publish(new ProfessorAtualizadoEvent(
            professor.Id,
            professor.Nome,
            professor.Email
        ));

        return Unit.Value;
    }
}
