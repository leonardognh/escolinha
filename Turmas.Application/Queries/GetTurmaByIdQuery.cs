using MediatR;
using Turmas.Application.DTOs;

namespace Turmas.Application.Queries;

public class GetTurmaByIdQuery : IRequest<TurmaDto?>
{
    public Guid Id { get; set; }

    public GetTurmaByIdQuery(Guid id)
    {
        Id = id;
    }
}
