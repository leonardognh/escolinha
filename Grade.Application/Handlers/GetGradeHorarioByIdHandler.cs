using Grade.Application.DTOs;
using Grade.Application.Queries;
using Grade.Domain.Interfaces;
using MediatR;

namespace Grade.Application.Handlers;

public class GetGradeHorariosByIdHandler : IRequestHandler<GetGradeHorariosByIdQuery, GradeHorariosDto?>
{
    private readonly IGradeHorariosRepository _repository;

    public GetGradeHorariosByIdHandler(IGradeHorariosRepository repository)
    {
        _repository = repository;
    }

    public async Task<GradeHorariosDto?> Handle(GetGradeHorariosByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);

        return entity is null ? null : new GradeHorariosDto
        {
            Id = entity.Id,
            TurmaId = entity.TurmaId,
            Bimestre = entity.Bimestre,
            DiaSemana = entity.DiaSemana,
            HorarioInicio = entity.HorarioInicio,
            HorarioFim = entity.HorarioFim,
            MateriaId = entity.MateriaId,
            ProfessorId = entity.ProfessorId
        };
    }
}
