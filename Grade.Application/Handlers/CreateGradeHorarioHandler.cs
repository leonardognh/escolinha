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
        var entity = new GradeHorario
        {
            Id = Guid.NewGuid(),
            TurmaId = request.TurmaId,
            Bimestre = request.Bimestre,
            DiaSemana = request.DiaSemana,
        };

        await _repository.AddAsync(entity);
        return entity.Id;
    }
}
