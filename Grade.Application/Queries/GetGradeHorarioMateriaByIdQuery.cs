using Grade.Application.DTOs;
using MediatR;

namespace Grade.Application.Queries;

public class GetGradeHorarioMateriaByIdQuery : IRequest<GradeHorarioMateriaDto?>
{
    public Guid GradeHorarioId { get; set; }

    public GetGradeHorarioMateriaByIdQuery(Guid gradeHorarioId)
    {
        GradeHorarioId = gradeHorarioId;
    }
}
