using MediatR;
using Materias.Application.DTOs;
using Materias.Application.Queries;
using Materias.Domain.Interfaces;

namespace Materias.Application.Handlers;

public class GetMateriaByIdHandler : IRequestHandler<GetMateriaByIdQuery, MateriaDto?>
{
    private readonly IMateriaRepository _repository;

    public GetMateriaByIdHandler(IMateriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<MateriaDto?> Handle(GetMateriaByIdQuery request, CancellationToken cancellationToken)
    {
        var materia = await _repository.GetByIdAsync(request.Id);
        return materia is null ? null : new MateriaDto
        {
            Id = materia.Id,
            Nome = materia.Nome,
        };
    }
}
