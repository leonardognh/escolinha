using MediatR;
using Materias.Application.DTOs;
using Materias.Application.Queries;
using Materias.Domain.Interfaces;

namespace Materias.Application.Handlers;

public class GetAllMateriasHandler : IRequestHandler<GetAllMateriasQuery, PaginatedResult<MateriaDto>>
{
    private readonly IMateriaRepository _repository;

    public GetAllMateriasHandler(IMateriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<MateriaDto>> Handle(GetAllMateriasQuery request, CancellationToken cancellationToken)
    {
        var (data, total) = await _repository.GetPagedAsync(request.Page, request.PageSize);

        return new PaginatedResult<MateriaDto>
        {
            Items = data.Select(x => new MateriaDto
            {
                Id = x.Id,
                Nome = x.Nome,
            }),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = total
        };
    }
}
