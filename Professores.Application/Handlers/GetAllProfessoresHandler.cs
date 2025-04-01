using MediatR;
using Professores.Application.DTOs;
using Professores.Application.Queries;
using Professores.Domain.Interfaces;

namespace Professores.Application.Handlers;

public class GetAllProfessoresHandler : IRequestHandler<GetAllProfessoresQuery, PaginatedResult<ProfessorDto>>
{
    private readonly IProfessorRepository _repository;

    public GetAllProfessoresHandler(IProfessorRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<ProfessorDto>> Handle(GetAllProfessoresQuery request, CancellationToken cancellationToken)
    {
        var (data, total) = await _repository.GetPagedAsync(request.Page, request.PageSize);

        return new PaginatedResult<ProfessorDto>
        {
            Items = data.Select(p => new ProfessorDto
            {
                Id = p.Id,
                Nome = p.Nome,
            }),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = total
        };
    }
}
