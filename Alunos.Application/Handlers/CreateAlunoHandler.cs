using Alunos.Application.Commands;
using Alunos.Domain.Entities;
using Alunos.Domain.Interfaces;
using IntegracaoMicroservicos.Contracts.Events.Alunos;
using MassTransit;
using MediatR;

public class CreateAlunoHandler : IRequestHandler<CreateAlunoCommand, Guid>
{
    private readonly IAlunoRepository _repository;
    private readonly IPublishEndpoint _publish;

    public CreateAlunoHandler(IAlunoRepository repository, IPublishEndpoint publish)
    {
        _repository = repository;
        _publish = publish;
    }

    public async Task<Guid> Handle(CreateAlunoCommand request, CancellationToken cancellationToken)
    {
        var aluno = new Aluno
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Email = request.Email,
            DataNascimento = request.DataNascimento,
            TurmaId = request.TurmaId
        };

        await _repository.AddAsync(aluno);

        await _publish.Publish(new AlunoCriadoEvent(aluno.Id, aluno.Nome, aluno.TurmaId));

        return aluno.Id;
    }
}
