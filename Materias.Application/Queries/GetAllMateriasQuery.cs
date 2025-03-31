using MediatR;
using Materias.Application.DTOs;

namespace Materias.Application.Queries;

public class GetAllMateriasQuery : IRequest<PaginatedResult<MateriaDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public GetAllMateriasQuery(int page = 1, int pageSize = 10)
    {
        Page = page;
        PageSize = pageSize;
    }
}
