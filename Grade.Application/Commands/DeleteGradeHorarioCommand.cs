using MediatR;

namespace Grade.Application.Commands;

public class DeleteGradeHorarioCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteGradeHorarioCommand(Guid id)
    {
        Id = id;
    }
}
