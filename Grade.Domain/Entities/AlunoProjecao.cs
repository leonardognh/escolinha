namespace Grade.Domain.Entities;

public class AlunoProjecao
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public Guid TurmaId { get; set; }
}
