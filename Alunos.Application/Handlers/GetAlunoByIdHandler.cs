using Alunos.Application.DTOs;
using Alunos.Application.Queries;
using Alunos.Domain.Interfaces;
using MediatR;

namespace Alunos.Application.Handlers;

public class GetAlunoByIdHandler : IRequestHandler<GetAlunoByIdQuery, AlunoDto?>
{
    private readonly IAlunoRepository _repository;

    public GetAlunoByIdHandler(IAlunoRepository repository)
    {
        _repository = repository;
    }

    public async Task<AlunoDto?> Handle(GetAlunoByIdQuery request, CancellationToken cancellationToken)
    {
        var aluno = await _repository.GetByIdAsync(request.Id);
        return aluno is null ? null : new AlunoDto
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            TurmaId = aluno.TurmaId
        };
    }
}
