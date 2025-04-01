using Grade.Application.Commands;
using Grade.Domain.Interfaces;
using MediatR;

namespace Grade.Application.Handlers;

public class UpdateGradeHorarioMateriaHandler : IRequestHandler<UpdateGradeHorarioMateriaCommand>
{
    private readonly IGradeHorarioMateriaRepository _repository;

    public UpdateGradeHorarioMateriaHandler(IGradeHorarioMateriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateGradeHorarioMateriaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity is null)
            throw new Exception("GradeHoraro Materia não encontrada.");

        entity.MateriaId = request.MateriaId;
        entity.ProfessorId = request.ProfessorId;

        await _repository.UpdateAsync(entity);
        return Unit.Value;
    }
}
