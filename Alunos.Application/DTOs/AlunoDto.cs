﻿namespace Alunos.Application.DTOs;

public class AlunoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public Guid TurmaId { get; set; }
}
