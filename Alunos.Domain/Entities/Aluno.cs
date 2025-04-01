namespace Alunos.Domain.Entities;

public class Aluno
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public Guid TurmaId { get; set; } 
}
