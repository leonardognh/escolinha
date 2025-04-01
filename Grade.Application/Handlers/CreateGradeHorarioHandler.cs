using Grade.Application.Commands;
using Grade.Domain.Entities;
using Grade.Domain.Interfaces;
using MediatR;

namespace Grade.Application.Handlers;

public class CreateGradeHorariosHandler : IRequestHandler<CreateGradeHorariosCommand, Guid>
{
    private readonly IGradeHorariosRepository _repository;

    public CreateGradeHorariosHandler(IGradeHorariosRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateGradeHorariosCommand request, CancellationToken cancellationToken)
    {
        var entity = new GradeHorarios
        {
            Id = Guid.NewGuid(),
            TurmaId = request.TurmaId,
            Bimestre = request.Bimestre,
            DiaSemana = request.DiaSemana,
            HorarioInicio = request.HorarioInicio,
            HorarioFim = request.HorarioFim,
            MateriaId = request.MateriaId,
            ProfessorId = request.ProfessorId
        };

        await _repository.AddAsync(entity);
        return entity.Id;
    }
}
