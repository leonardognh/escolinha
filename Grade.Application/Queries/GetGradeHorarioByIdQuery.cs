using Grade.Application.DTOs;
using MediatR;

namespace Grade.Application.Queries;

public class GetGradeHorariosByIdQuery : IRequest<GradeHorariosDto?>
{
    public Guid Id { get; set; }

    public GetGradeHorariosByIdQuery(Guid id)
    {
        Id = id;
    }
}
