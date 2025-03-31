using MediatR;
using Professores.Application.DTOs;

namespace Professores.Application.Queries;

public class GetProfessorByIdQuery : IRequest<ProfessorDto>
{
    public Guid Id { get; set; }

    public GetProfessorByIdQuery(Guid id)
    {
        Id = id;
    }
}
