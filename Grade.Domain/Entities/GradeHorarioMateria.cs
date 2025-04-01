using Grade.Domain.Entities.Projecao;

namespace Grade.Domain.Entities;

public class GradeHorarioMateria
{
    public Guid Id { get; set; }
    public Guid MateriaId { get; set; }
    public Guid ProfessorId { get; set; }

    public GradeHorario GradeHorario { get; set; } = null!;
    public MateriaProjecao Materia { get; set; } = null!;
    public ProfessorProjecao Professor { get; set; } = null!;
}
