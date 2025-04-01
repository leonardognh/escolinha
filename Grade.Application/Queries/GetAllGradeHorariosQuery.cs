using Grade.Application.DTOs;
using MediatR;

namespace Grade.Application.Queries;

public class GetAllGradeHorariosQuery : IRequest<PaginatedResult<GradeHorariosDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public GetAllGradeHorariosQuery(int page = 1, int pageSize = 10)
    {
        Page = page;
        PageSize = pageSize;
    }
}
