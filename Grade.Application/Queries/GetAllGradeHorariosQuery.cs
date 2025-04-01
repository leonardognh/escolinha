using Grade.Application.DTOs;
using MediatR;

namespace Grade.Application.Queries;

public class GetAllGradeHorariossQuery : IRequest<PaginatedResult<GradeHorariosDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public GetAllGradeHorariossQuery(int page = 1, int pageSize = 10)
    {
        Page = page;
        PageSize = pageSize;
    }
}
