namespace Contracts.Events.Turmas;

public record TurmaCriadaEvent(Guid Id, string Nome, int Ano, string? Turno);
