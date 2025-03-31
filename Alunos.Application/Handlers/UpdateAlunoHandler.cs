using Alunos.Application.Commands;
using Alunos.Domain.Interfaces;
using MediatR;

namespace Alunos.Application.Handlers;

public class UpdateAlunoHandler : IRequestHandler<UpdateAlunoCommand>
{
    private readonly IAlunoRepository _repository;

    public UpdateAlunoHandler(IAlunoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateAlunoCommand request, CancellationToken cancellationToken)
    {
        var aluno = await _repository.GetByIdAsync(request.Id);
        if (aluno is null)
            throw new Exception("Aluno não encontrado.");

        aluno.Nome = request.Nome;
        aluno.Email = request.Email;
        aluno.DataNascimento = request.DataNascimento;
        aluno.TurmaId = request.TurmaId;

        await _repository.UpdateAsync(aluno);
        return Unit.Value;
    }
}
