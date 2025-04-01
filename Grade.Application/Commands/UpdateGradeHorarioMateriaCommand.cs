using MediatR;

namespace Grade.Application.Commands;

public class UpdateGradeHorarioMateriaCommand : IRequest
{
    public Guid GradeHorarioId { get; set; }
    public Guid MateriaId { get; set; }
    public Guid ProfessorId { get; set; }
}
