using MediatR;

namespace Grade.Application.Commands;

public class CreateGradeHorarioMateriaCommand : IRequest<Guid>
{
    public Guid MateriaId { get; set; }
    public Guid ProfessorId { get; set; }
}
