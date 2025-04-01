using Grade.Application.Commands;
using Grade.Domain.Interfaces;
using MediatR;

namespace Grade.Application.Handlers;

public class UpdateGradeHorariosHandler : IRequestHandler<UpdateGradeHorariosCommand>
{
    private readonly IGradeHorariosRepository _repository;

    public UpdateGradeHorariosHandler(IGradeHorariosRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateGradeHorariosCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity is null)
            throw new Exception("Grade não encontrada.");

        entity.TurmaId = request.TurmaId;
        entity.Bimestre = request.Bimestre;
        entity.DiaSemana = request.DiaSemana;
        entity.HorarioInicio = request.HorarioInicio;
        entity.HorarioFim = request.HorarioFim;
        entity.MateriaId = request.MateriaId;
        entity.ProfessorId = request.ProfessorId;

        await _repository.UpdateAsync(entity);
        return Unit.Value;
    }
}
