namespace Turmas.Domain.Entities;

public class Turma
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }
}
