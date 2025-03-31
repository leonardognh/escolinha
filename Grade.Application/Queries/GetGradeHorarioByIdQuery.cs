using Grade.Application.DTOs;
using MediatR;

namespace Grade.Application.Queries;

public class GetGradeHorarioByIdQuery : IRequest<GradeHorarioDto?>
{
    public Guid Id { get; set; }

    public GetGradeHorarioByIdQuery(Guid id)
    {
        Id = id;
    }
}
