using Grade.Application.DTOs;
using Grade.Application.Queries;
using Grade.Domain.Interfaces;
using MediatR;

namespace Grade.Application.Handlers;

public class GetGradeHorarioMateriaByIdHandler : IRequestHandler<GetGradeHorarioMateriaByIdQuery, GradeHorarioMateriaDto?>
{
    private readonly IGradeHorarioMateriaRepository _repository;

    public GetGradeHorarioMateriaByIdHandler(IGradeHorarioMateriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<GradeHorarioMateriaDto?> Handle(GetGradeHorarioMateriaByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);

        return entity is null ? null : new GradeHorarioMateriaDto
        {
            Id = entity.Id,
            MateriaId = entity.MateriaId,
            ProfessorId = entity.ProfessorId,
        };
    }
}