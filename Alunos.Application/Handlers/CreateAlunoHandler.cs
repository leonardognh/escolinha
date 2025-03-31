using Alunos.Application.Commands;
using Alunos.Domain.Entities;
using Alunos.Domain.Interfaces;
using MediatR;

namespace Alunos.Application.Handlers;

public class CreateAlunoHandler : IRequestHandler<CreateAlunoCommand, Guid>
{
    private readonly IAlunoRepository _repository;

    public CreateAlunoHandler(IAlunoRepository repository)
    {
        _repository = repository;
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
        return aluno.Id;
    }
}
