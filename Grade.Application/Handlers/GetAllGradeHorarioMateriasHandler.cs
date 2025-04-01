using Grade.Application.DTOs;
using Grade.Application.Queries;
using Grade.Domain.Interfaces;
using MediatR;

namespace Grade.Application.Handlers;

public class GetAllGradeHorarioMateriasHandler : IRequestHandler<GetAllGradeHorarioMateriasQuery, PaginatedResult<GradeHorarioMateriaDto>>
{
    private readonly IGradeHorarioMateriaRepository _repository;

    public GetAllGradeHorarioMateriasHandler(IGradeHorarioMateriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<GradeHorarioMateriaDto>> Handle(GetAllGradeHorarioMateriasQuery request, CancellationToken cancellationToken)
    {
        var (data, total) = await _repository.GetPagedAsync(request.Page, request.PageSize);

        return new PaginatedResult<GradeHorarioMateriaDto>
        {
            Items = data.Select(x => new GradeHorarioMateriaDto
            {
                Id = x.Id,
                MateriaId = x.MateriaId,
                ProfessorId = x.ProfessorId,
            }),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = total
        };
    }
}
