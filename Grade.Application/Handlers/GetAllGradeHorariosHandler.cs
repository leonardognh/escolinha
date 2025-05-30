﻿using Grade.Application.DTOs;
using Grade.Application.Queries;
using Grade.Domain.Interfaces;
using MediatR;

namespace Grade.Application.Handlers;

public class GetAllGradeHorariosHandler : IRequestHandler<GetAllGradeHorariosQuery, PaginatedResult<GradeHorariosDto>>
{
    private readonly IGradeHorariosRepository _repository;

    public GetAllGradeHorariosHandler(IGradeHorariosRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<GradeHorariosDto>> Handle(GetAllGradeHorariosQuery request, CancellationToken cancellationToken)
    {
        var (data, total) = await _repository.GetPagedAsync(request.Page, request.PageSize);

        return new PaginatedResult<GradeHorariosDto>
        {
            Items = data.Select(x => new GradeHorariosDto
            {
                Id = x.Id,
                TurmaId = x.TurmaId,
                Bimestre = x.Bimestre,
                DiaSemana = x.DiaSemana,
            }),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = total
        };
    }
}
