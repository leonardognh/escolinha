using MediatR;

namespace Grade.Application.Commands;

public class DeleteGradeHorariosCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteGradeHorariosCommand(Guid id)
    {
        Id = id;
    }
}
