using Alunos.Application.DTOs;
using Alunos.Application.Queries;
using Alunos.Domain.Interfaces;
using MediatR;

namespace Alunos.Application.Handlers;

public class GetAllAlunosHandler : IRequestHandler<GetAllAlunosQuery, PaginatedResult<AlunoDto>>
{
    private readonly IAlunoRepository _repository;

    public GetAllAlunosHandler(IAlunoRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<AlunoDto>> Handle(GetAllAlunosQuery request, CancellationToken cancellationToken)
    {
        var (data, total) = await _repository.GetPagedAsync(request.Page, request.PageSize);

        return new PaginatedResult<AlunoDto>
        {
            Items = data.Select(a => new AlunoDto
            {
                Id = a.Id,
                Nome = a.Nome,
                Email = a.Email,
                DataNascimento = a.DataNascimento,
                TurmaId = a.TurmaId
            }),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = total
        };
    }
}
