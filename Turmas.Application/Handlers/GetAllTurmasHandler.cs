using MediatR;
using Turmas.Application.DTOs;
using Turmas.Application.Queries;
using Turmas.Domain.Interfaces;

namespace Turmas.Application.Handlers;

public class GetAllTurmasHandler : IRequestHandler<GetAllTurmasQuery, PaginatedResult<TurmaDto>>
{
    private readonly ITurmaRepository _repository;

    public GetAllTurmasHandler(ITurmaRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<TurmaDto>> Handle(GetAllTurmasQuery request, CancellationToken cancellationToken)
    {
        var (data, total) = await _repository.GetPagedAsync(request.Page, request.PageSize);

        return new PaginatedResult<TurmaDto>
        {
            Items = data.Select(x => new TurmaDto
            {
                Id = x.Id,
                Nome = x.Nome,
                Ano = x.Ano,
                Turno = x.Turno
            }),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = total
        };
    }
}
