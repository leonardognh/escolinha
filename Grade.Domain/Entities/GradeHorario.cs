using Grade.Domain.Enums;

namespace Grade.Domain.Entities;

public class GradeHorario
{
    public Guid Id { get; set; }
    public Guid TurmaId { get; set; }
    public int Bimestre { get; set; }
    public DiaSemana DiaSemana { get; set; }

    public ICollection<GradeHorarioMateria> Materias { get; set; } = new List<GradeHorarioMateria>();
}
