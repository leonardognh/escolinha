using MediatR;
using Professores.Application.DTOs;

namespace Professores.Application.Queries;

public class GetAllProfessoresQuery : IRequest<PaginatedResult<ProfessorDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public GetAllProfessoresQuery(int page = 1, int pageSize = 10)
    {
        Page = page;
        PageSize = pageSize;
    }
}
