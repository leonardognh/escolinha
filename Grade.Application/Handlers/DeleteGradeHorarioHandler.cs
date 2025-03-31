using Grade.Application.Commands;
using Grade.Domain.Interfaces;
using MediatR;

namespace Grade.Application.Handlers;

public class DeleteGradeHorarioHandler : IRequestHandler<DeleteGradeHorarioCommand>
{
    private readonly IGradeHorarioRepository _repository;

    public DeleteGradeHorarioHandler(IGradeHorarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteGradeHorarioCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
