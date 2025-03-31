namespace Materias.Domain.Entities;

public class Materia
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public int? CargaHoraria { get; set; }
}
