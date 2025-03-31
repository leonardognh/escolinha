using MediatR;
using Alunos.Application.DTOs;

namespace Alunos.Application.Queries;

public class GetAlunoByIdQuery : IRequest<AlunoDto?>
{
    public Guid Id { get; set; }

    public GetAlunoByIdQuery(Guid id) => Id = id;
}
