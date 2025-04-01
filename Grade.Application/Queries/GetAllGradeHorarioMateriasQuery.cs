using Grade.Application.DTOs;
using MediatR;

namespace Grade.Application.Queries;

public class GetAllGradeHorarioMateriasQuery : IRequest<PaginatedResult<GradeHorarioMateriaDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public GetAllGradeHorarioMateriasQuery(int page = 1, int pageSize = 10)
    {
        Page = page;
        PageSize = pageSize;
    }
}
