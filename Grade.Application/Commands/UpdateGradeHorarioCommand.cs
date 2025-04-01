using Grade.Domain.Enums;
using MediatR;

namespace Grade.Application.Commands;

public class UpdateGradeHorariosCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid TurmaId { get; set; }
    public int Bimestre { get; set; }
    public DiaSemana DiaSemana { get; set; }
}
