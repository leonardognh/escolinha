using Grade.Application.Commands;
using Grade.Domain.Interfaces;
using MediatR;

namespace Grade.Application.Handlers;

public class DeleteGradeHorariosHandler : IRequestHandler<DeleteGradeHorariosCommand>
{
    private readonly IGradeHorariosRepository _repository;

    public DeleteGradeHorariosHandler(IGradeHorariosRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteGradeHorariosCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
