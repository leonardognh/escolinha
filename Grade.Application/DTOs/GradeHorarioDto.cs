﻿using Grade.Domain.Enums;

namespace Grade.Application.DTOs;

public class GradeHorarioDto
{
    public Guid Id { get; set; }
    public Guid TurmaId { get; set; }
    public int Bimestre { get; set; }
    public DiaSemana DiaSemana { get; set; }
    public TimeSpan HorarioInicio { get; set; }
    public TimeSpan HorarioFim { get; set; }
    public Guid MateriaId { get; set; }
    public Guid ProfessorId { get; set; }
}
