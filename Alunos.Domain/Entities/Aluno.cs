namespace Alunos.Domain.Entities;

public class Aluno
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public Guid TurmaId { get; set; } 
}
