using Grade.Domain.Entities;
using Grade.Domain.Interfaces;
using MediatR;

namespace Grade.Application.Commands;

public class CreateGradeHorarioMateriaHandler : IRequestHandler<CreateGradeHorarioMateriaCommand, Guid>
{
    private readonly IGradeHorarioMateriaRepository _repository;

    public CreateGradeHorarioMateriaHandler(IGradeHorarioMateriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateGradeHorarioMateriaCommand request, CancellationToken cancellationToken)
    {
        var entity = new GradeHorarioMateria
        {
            GradeHorarioId = request.GradeHorarioId,
            MateriaId = request.MateriaId,
            ProfessorId = request.ProfessorId,
        };

        await _repository.AddAsync(entity);
        return entity.GradeHorarioId;
    }
}
