using MediatR;
using Professores.Application.DTOs;
using Professores.Application.Queries;
using Professores.Domain.Interfaces;

namespace Professores.Application.Handlers;

public class GetProfessorByIdHandler : IRequestHandler<GetProfessorByIdQuery, ProfessorDto?>
{
    private readonly IProfessorRepository _repository;

    public GetProfessorByIdHandler(IProfessorRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProfessorDto?> Handle(GetProfessorByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);

        return entity is null ? null : new ProfessorDto
        {
            Id = entity.Id,
            Nome = entity.Nome,
            Email = entity.Email,
            Telefone = entity.Telefone
        };
    }
}
