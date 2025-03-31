namespace Contracts.Events.Materias;
public record MateriaCriadaEvent(Guid Id, string Nome, string? Descricao, int? CargaHoraria);
