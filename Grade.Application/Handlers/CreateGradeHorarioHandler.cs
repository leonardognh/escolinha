using Grade.Application.Commands;
using Grade.Domain.Entities;
using Grade.Domain.Interfaces;
using MediatR;

namespace Grade.Application.Handlers;

public class CreateGradeHorarioHandler : IRequestHandler<CreateGradeHorarioCommand, Guid>
{
    private readonly IGradeHorarioRepository _repository;

    public CreateGradeHorarioHandler(IGradeHorarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateGradeHorarioCommand request, CancellationToken cancellationToken)
    {
        var entity = new GradeHorario
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
