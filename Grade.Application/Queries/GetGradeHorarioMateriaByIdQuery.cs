using Grade.Application.DTOs;
using Grade.Domain.Entities;
using MediatR;

namespace Grade.Application.Queries;

public class GetGradeHorarioMateriaByIdQuery : IRequest<GradeHorarioMateriaDto?>
{
    public Guid Id { get; set; }

    public GetGradeHorarioMateriaByIdQuery(Guid id)
    {
        Id = id;
    }
}
