using MediatR;
using Turmas.Application.DTOs;

namespace Turmas.Application.Queries;

public class GetAllTurmasQuery : IRequest<PaginatedResult<TurmaDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public GetAllTurmasQuery(int page = 1, int pageSize = 10)
    {
        Page = page;
        PageSize = pageSize;
    }
}
