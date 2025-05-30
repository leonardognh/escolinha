﻿using Grade.Domain.Enums;

namespace Grade.Application.DTOs;

public class GradeHorariosDto
{
    public Guid Id { get; set; }
    public Guid TurmaId { get; set; }
    public int Bimestre { get; set; }
    public DiaSemana DiaSemana { get; set; }
}
