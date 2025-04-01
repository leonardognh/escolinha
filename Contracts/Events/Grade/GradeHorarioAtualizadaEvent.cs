namespace Contracts.Events.Grade;

public record GradeHorariosAtualizadaEvent(
    Guid TurmaId,
    int Bimestre,
    IEnumerable<AulaDto> Aulas
);

public record AulaDto(
    Guid Id,
    string MateriaNome,
    string ProfessorNome,
    int DiaSemana
);