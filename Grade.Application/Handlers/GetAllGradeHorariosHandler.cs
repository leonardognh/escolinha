using Grade.Application.DTOs;
using Grade.Application.Queries;
using Grade.Domain.Interfaces;
using MediatR;

namespace Grade.Application.Handlers;

public class GetAllGradeHorariosHandler : IRequestHandler<GetAllGradeHorariosQuery, PaginatedResult<GradeHorarioDto>>
{
    private readonly IGradeHorarioRepository _repository;

    public GetAllGradeHorariosHandler(IGradeHorarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<GradeHorarioDto>> Handle(GetAllGradeHorariosQuery request, CancellationToken cancellationToken)
    {
        var (data, total) = await _repository.GetPagedAsync(request.Page, request.PageSize);

        return new PaginatedResult<GradeHorarioDto>
        {
            Items = data.Select(x => new GradeHorarioDto
            {
                Id = x.Id,
                TurmaId = x.TurmaId,
                Bimestre = x.Bimestre,
                DiaSemana = x.DiaSemana,
                HorarioInicio = x.HorarioInicio,
                HorarioFim = x.HorarioFim,
                MateriaId = x.MateriaId,
                ProfessorId = x.ProfessorId
            }),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = total
        };
    }
}
