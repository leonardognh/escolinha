using MediatR;
using Materias.Application.DTOs;

namespace Materias.Application.Queries;

public class GetMateriaByIdQuery : IRequest<MateriaDto?>
{
    public Guid Id { get; set; }

    public GetMateriaByIdQuery(Guid id)
    {
        Id = id;
    }
}
