using MediatR;
using Turmas.Application.DTOs;
using Turmas.Application.Queries;
using Turmas.Domain.Interfaces;

namespace Turmas.Application.Handlers;

public class GetTurmaByIdHandler : IRequestHandler<GetTurmaByIdQuery, TurmaDto?>
{
    private readonly ITurmaRepository _repository;

    public GetTurmaByIdHandler(ITurmaRepository repository)
    {
        _repository = repository;
    }

    public async Task<TurmaDto?> Handle(GetTurmaByIdQuery request, CancellationToken cancellationToken)
    {
        var turma = await _repository.GetByIdAsync(request.Id);
        return turma is null ? null : new TurmaDto
        {
            Id = turma.Id,
            Nome = turma.Nome,
            Ano = turma.Ano,
            Turno = turma.Turno
        };
    }
}
