namespace Turmas.Application.DTOs;

public class TurmaDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }
}
