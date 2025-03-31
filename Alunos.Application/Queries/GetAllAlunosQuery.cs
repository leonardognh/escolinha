using MediatR;
using Alunos.Application.DTOs;

namespace Alunos.Application.Queries;

public class GetAllAlunosQuery : IRequest<PaginatedResult<AlunoDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public GetAllAlunosQuery(int page = 1, int pageSize = 10)
    {
        Page = page;
        PageSize = pageSize;
    }
}
