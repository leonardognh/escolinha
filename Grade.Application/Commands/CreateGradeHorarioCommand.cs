using Grade.Domain.Enums;
using MediatR;

namespace Grade.Application.Commands;

public class CreateGradeHorarioCommand : IRequest<Guid>
{
    public Guid TurmaId { get; set; }
    public int Bimestre { get; set; }
    public DiaSemana DiaSemana { get; set; }
    public TimeSpan HorarioInicio { get; set; }
    public TimeSpan HorarioFim { get; set; }
    public Guid MateriaId { get; set; }
    public Guid ProfessorId { get; set; }
}
