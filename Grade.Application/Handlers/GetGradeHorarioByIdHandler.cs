using Grade.Application.DTOs;
using Grade.Application.Queries;
using Grade.Domain.Interfaces;
using MediatR;

namespace Grade.Application.Handlers;

public class GetGradeHorarioByIdHandler : IRequestHandler<GetGradeHorarioByIdQuery, GradeHorarioDto?>
{
    private readonly IGradeHorarioRepository _repository;

    public GetGradeHorarioByIdHandler(IGradeHorarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<GradeHorarioDto?> Handle(GetGradeHorarioByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);

        return entity is null ? null : new GradeHorarioDto
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
