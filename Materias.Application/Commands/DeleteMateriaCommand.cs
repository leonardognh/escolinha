using MediatR;

namespace Materias.Application.Commands;

public class DeleteMateriaCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteMateriaCommand(Guid id)
    {
        Id = id;
    }
}
