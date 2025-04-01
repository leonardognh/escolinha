using Alunos.Application.Commands;
using Alunos.Domain.Interfaces;
using Contracts.Events.Alunos;
using MassTransit;
using MediatR;

public class UpdateAlunoHandler : IRequestHandler<UpdateAlunoCommand>
{
    private readonly IAlunoRepository _repository;
    private readonly IPublishEndpoint _publish;

    public UpdateAlunoHandler(IAlunoRepository repository, IPublishEndpoint publish)
    {
        _repository = repository;
        _publish = publish;
    }

    public async Task<Unit> Handle(UpdateAlunoCommand request, CancellationToken cancellationToken)
    {
        var aluno = await _repository.GetByIdAsync(request.Id)
            ?? throw new Exception("Aluno não encontrado.");

        var turmaAnteriorId = aluno.TurmaId;

        aluno.Nome = request.Nome;
        aluno.TurmaId = request.TurmaId;

        await _repository.UpdateAsync(aluno);

        if (turmaAnteriorId != aluno.TurmaId)
        {
            await _publish.Publish(new AlunoTransferidoEvent(aluno.Id, turmaAnteriorId, aluno.TurmaId));
        }

        return Unit.Value;
    }
}
